using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using ServiceContract.Interfaces;

namespace AIMP_v3._0.DataAccess
{
    public abstract class BaseHttpServer<TService> : IDisposable
        where TService : class
    {
        private EndpointAddress _address;
        private WebHttpBinding _binding;
        private WebHttpBehavior _behavior;
        private OperationContextScope _scope;
        private ChannelFactory<TService> _channelFactory;

        protected readonly TService Proxy;

        public BaseHttpServer(Uri uri)
        {
            _address = new EndpointAddress(uri);

            _binding = new WebHttpBinding();

            _binding.MaxReceivedMessageSize = int.MaxValue;

            _behavior = new WebHttpBehavior();

            _channelFactory = new ChannelFactory<TService>(_binding, _address);
            _channelFactory.Endpoint.Behaviors.Add(_behavior);

            Proxy = _channelFactory.CreateChannel(_address);

            _scope = new OperationContextScope((IContextChannel)Proxy);            
        }

        public void Dispose()
        {
            if(_channelFactory != null)
                if(_channelFactory.State == CommunicationState.Opened)
                    _channelFactory.Close();

            if (_scope != null)
                _scope.Dispose();
        }
    }
}
