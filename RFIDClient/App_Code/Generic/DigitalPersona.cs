using System;
using System.Collections.Generic;
using System.Text;
using DPUruNet;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
namespace Generic
{
    public class DigitalPersona
    {
        internal Reader oReader;
        /// <summary>
        /// Open a device and check result for errors.
        /// </summary>
        /// <returns>Returns true if successful; false if unsuccessful</returns>
        public bool OpenReader()
        {
            ReaderCollection readerList = ReaderCollection.GetReaders();
            if (readerList == null || readerList.Count == 0)
            {
                IsConnect = false;
                return false;
            }
            oReader = readerList[0];
            Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;
            result = oReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
            if (result != Constants.ResultCode.DP_SUCCESS)
            {
                IsConnect = false;
                return false;
            }
            IsConnect = true;
            return true;
        }

        public bool IsConnect { set; get; }

        public bool StartCaptureAsync(Reader.CaptureCallback OnCaptured)
        {
            try
            {
                oReader.On_Captured += new Reader.CaptureCallback(OnCaptured);
                //if (oReader.Status != null && oReader.Status.Status.Equals(Constants.ReaderStatuses.DP_STATUS_BUSY))
                //    return false;
                if (!CaptureFingerAsync())
                    return false;
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// Function to capture a finger. Always get status first and calibrate or wait if necessary.  Always check status and capture errors.
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public bool CaptureFingerAsync()
        {
            try
            {
                GetStatus();
                Constants.ResultCode captureResult = oReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, oReader.Capabilities.Resolutions[0]);
                if (captureResult != Constants.ResultCode.DP_SUCCESS)
                {
                    return IsConnect ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:  " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Cancel the capture and then close the reader.
        /// </summary>
        /// <param name="OnCaptured">Delegate to unhook as handler of the On_Captured event </param>
        public void CancelCaptureAndCloseReader(Reader.CaptureCallback OnCaptured)
        {
            if (oReader != null)
            {
                oReader.CancelCapture();
                oReader.Dispose();
            }
        }

        /// <summary>
        /// Check the device status before starting capture.
        /// </summary>
        /// <returns></returns>
        public void GetStatus()
        {
            Constants.ResultCode result = oReader.GetStatus();
            if ((result != Constants.ResultCode.DP_SUCCESS))
            {
                throw new Exception("" + result);
            }
            if ((oReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY))
            {
                Thread.Sleep(50);
            }
            else if ((oReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
            {
                oReader.Calibrate();
            }
            else if ((oReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
            {
                throw new Exception("Reader Status - " + oReader.Status.Status);
            }
        }

        /// <summary>
        /// Handler for when a fingerprint is captured.
        /// </summary>
        /// <param name="captureResult">contains info and data on the fingerprint capture</param>
        public void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                if (!CheckCaptureResult(captureResult)) return;
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
                var tmp = Fmd.SerializeXml(resultConversion.Data);

                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {
                    //SendMessage(Action.SendBitmap, CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height));
                }
            }
            catch (Exception ex)
            {
                //SendMessage(Action.SendMessage, "Error:  " + ex.Message);
            }
        }

        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }
            bmp.UnlockBits(data);
            return bmp;
        }

        /// <summary>
        /// Check quality of the resulting capture.
        /// </summary>
        public bool CheckCaptureResult(CaptureResult captureResult)
        {
            if (captureResult.Data == null)
            {
                if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    throw new Exception(captureResult.ResultCode.ToString());
                }
                if ((captureResult.Quality != Constants.CaptureQuality.DP_QUALITY_CANCELED))
                {
                    throw new Exception("Quality - " + captureResult.Quality);
                }
                return false;
            }
            return true;
        }

    }
}
