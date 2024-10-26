using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;
namespace Generic
{
    public class PropName
    {
        public string LangType { get; set; }
        public string Country { get; set; }
        public string Vendor { get; set; }
        public string UsrId { get; set; }
        public string LocalIP { get; set; }
        public string ReaderType { get; set; }
        public string ComPort { get; set; }
        public string StepNo { get; set; }
        public string AntennaSequence { get; set; }
        public string RfAttenuation { get; set; }
        public string RssiFilter { get; set; }
        public string RfLevel { get; set; }
        // For UHF
        public string ReadPower { get; set; }
        public string WritePower { get; set; }
        public string WriteTime { get; set; }
    }
    /// <summary>
    /// Purpose: Read and write JSON file
    /// Author:   Hanxing
    /// Created: 2018-3-26
    /// Modifier: 
    /// Modified: 
    /// </summary>
    class ReadWriteJson
    {
        string jsonText = "";
        string jsonPath = AppDomain.CurrentDomain.BaseDirectory + "LoginInfo.json";

        public ReadWriteJson() { CreateFile(); }

        private void CreateFile()
        {
            if (!File.Exists(jsonPath))
            {
                string jsonTemp = JsonConvert.SerializeObject(new PropName());
                StreamWriter oSrw = new StreamWriter(jsonPath, false, Encoding.UTF8);
                oSrw.Write("[" + jsonTemp + "]");
                oSrw.Close();
                oSrw.Dispose();
            }
            jsonText = File.ReadAllText(jsonPath);
        }

        /// <summary>
        /// 讀取 Json，返回集合
        /// </summary>
        /// <returns></returns>
        public List<PropName> ReadJson()
        {
            if (jsonText.Trim() == "") { return null; }
            List<PropName> listJson = JsonConvert.DeserializeObject<List<PropName>>(jsonText);
            return listJson;
        }

        /// <summary>
        /// 寫入 Json
        /// </summary>
        /// <param name="arrPara"></param>
        /// <returns></returns>
        public bool WriteJson(PropName oPropName)
        {
            bool bSave = false;
            try
            {
                List<PropName> listJson = ReadJson();
                if (listJson.Count > 0 && oPropName != null)
                {
                    StringBuilder oSb = new StringBuilder();
                    StringWriter oSw = new StringWriter(oSb);
                    using (JsonWriter oJw = new JsonTextWriter(oSw))
                    {
                        oJw.Formatting = Formatting.None;
                        oJw.WriteStartArray();
                        oJw.WriteStartObject();
                        oJw.WritePropertyName("LangType");
                        oJw.WriteValue(ChkVal(oPropName.LangType, listJson[0].LangType));
                        oJw.WritePropertyName("Country");
                        oJw.WriteValue(ChkVal(oPropName.Country, listJson[0].Country));
                        oJw.WritePropertyName("Vendor");
                        oJw.WriteValue(ChkVal(oPropName.Vendor, listJson[0].Vendor));
                        oJw.WritePropertyName("UsrId");
                        oJw.WriteValue(ChkVal(oPropName.UsrId, listJson[0].UsrId));
                        oJw.WritePropertyName("LocalIP");
                        oJw.WriteValue(ChkVal(oPropName.LocalIP, listJson[0].LocalIP));
                        oJw.WritePropertyName("ComPort");
                        oJw.WriteValue(ChkVal(oPropName.ComPort, listJson[0].ComPort));
                        oJw.WritePropertyName("ReaderType");
                        oJw.WriteValue(ChkVal(oPropName.ReaderType, listJson[0].ReaderType));
                        oJw.WritePropertyName("StepNo");
                        oJw.WriteValue(ChkVal(oPropName.StepNo, listJson[0].StepNo));
                        oJw.WritePropertyName("AntennaSequence");
                        oJw.WriteValue(ChkVal(oPropName.AntennaSequence, listJson[0].AntennaSequence));
                        oJw.WritePropertyName("RfAttenuation");
                        oJw.WriteValue(ChkVal(oPropName.RfAttenuation, listJson[0].RfAttenuation));
                        oJw.WritePropertyName("RssiFilter");
                        oJw.WriteValue(ChkVal(oPropName.RssiFilter, listJson[0].RssiFilter));
                        oJw.WritePropertyName("RfLevel");
                        oJw.WriteValue(ChkVal(oPropName.RfLevel, listJson[0].RfLevel));
                        oJw.WritePropertyName("ReadPower");
                        oJw.WriteValue(ChkVal(oPropName.ReadPower, listJson[0].ReadPower));
                        oJw.WritePropertyName("WritePower");
                        oJw.WriteValue(ChkVal(oPropName.WritePower, listJson[0].WritePower));
                        oJw.WritePropertyName("WriteTime");
                        oJw.WriteValue(ChkVal(oPropName.WriteTime, listJson[0].WriteTime));
                        oJw.WriteEndObject();
                        oJw.WriteEndArray();
                    }
                    StreamWriter oSrw = new StreamWriter(jsonPath, false, Encoding.UTF8);
                    oSrw.Write(oSb);
                    oSrw.Close();
                    oSrw.Dispose();
                    oSb.Clear();
                    bSave = true;
                }
            }
            catch { bSave = false; }
            return bSave;
        }

        public string ChkVal(object objNew, object objOld)
        {
            return objNew == null || objNew.ToString() == "" ? Convert.ToString(objOld) : objNew.ToString();
        }
    }

}
