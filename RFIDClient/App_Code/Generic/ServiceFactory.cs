using System;
using System.ServiceModel;
namespace Generic
{
    public class ServiceFactory<T> where T : class
    {
        /// <summary> 創建通道服務 </summary>
        public static T GetService()
        {
            Common.ServerURI = "net.tcp://" + AppConfig.CurrentCountry;
            string address = Common.ServerURI + "/rfid";
            ChannelFactory<T> oFactory = null;
            T oService = null;
            string typeName = typeof(T).ToString();
            typeName = typeName.Substring(typeName.LastIndexOf(".") + 1);
            try
            {
                NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.None);
                netTcpBinding.MaxBufferSize = int.MaxValue;
                netTcpBinding.MaxBufferPoolSize = int.MaxValue;
                netTcpBinding.MaxReceivedMessageSize = int.MaxValue;
                netTcpBinding.CloseTimeout = TimeSpan.FromMinutes(15);
                netTcpBinding.OpenTimeout = TimeSpan.FromMinutes(15);
                //netTcpBinding.ReceiveTimeout = TimeSpan.FromHours(24);  //防止WCF默认十分钟剔除
                //netTcpBinding.ReliableSession.InactivityTimeout = TimeSpan.FromHours(24); //防止WCF默认十分钟剔除
                netTcpBinding.TransactionFlow = true;
                netTcpBinding.TransactionProtocol = TransactionProtocol.WSAtomicTransactionOctober2004;
                //oFactory = new ChannelFactory<T>(netTcpBinding, new EndpointAddress(address + "/" + typeName));
                InstanceContext oContext = new InstanceContext(new CallbackHandler());
                oFactory = new DuplexChannelFactory<T>(oContext, netTcpBinding, new EndpointAddress(address + "/" + typeName));
                oService = oFactory.CreateChannel();
            }
            catch (Exception ex)
            {
                oFactory = null;
                oService = null;
                throw new Exception("GetService not work. Cause :" + ex.Message, ex);
            }
            return oService;
        }

    }
}
