using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using RFIDModel.Common;

namespace Generic
{
    class ServiceClientSend : ClientBase<IClientSendMsg>, IClientSendMsg
    {
        public ServiceClientSend(InstanceContext callback, Binding binding, EndpointAddress address) : base(callback, binding, address) { }

        public void ClientSendMsg(string message)
        {
            Channel.ClientSendMsg(System.Net.Dns.GetHostName() + "-" + message);
        }

    }
}
