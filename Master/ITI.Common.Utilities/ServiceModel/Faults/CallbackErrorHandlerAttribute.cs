using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Configuration;

namespace ITI.Common.Utilities.ServiceModel.Faults
{
    public class CallbackErrorHandlerBehaviorAttribute : ErrorHandlerBehaviorAttribute, IEndpointBehavior
    {
        public CallbackErrorHandlerBehaviorAttribute(Type clientType)
        {
            ServiceType = clientType;
        }

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
        { }
        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
        {
            //behavior..ChannelDispatcher.ErrorHandlers.Add(this);
        }
        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher dispatcher)
        {
            dispatcher.ChannelDispatcher.ErrorHandlers.Add(this);
        }
        void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
        { }
    }    
}
