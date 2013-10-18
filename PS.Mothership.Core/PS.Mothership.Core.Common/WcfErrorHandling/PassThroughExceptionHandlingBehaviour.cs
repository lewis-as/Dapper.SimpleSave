﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using log4net;

namespace PS.Mothership.Core.Common.WcfErrorHandling
{
    public class PassThroughExceptionHandlingBehaviour : Attribute, IClientMessageInspector, IErrorHandler,
    IEndpointBehavior, IServiceBehavior, IContractBehavior
    {
        private readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
      

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (reply.IsFault)
            {
                // Create a copy of the original reply to allow default processing of the message
                MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);
                Message copy = buffer.CreateMessage();  // Create a copy to work with
                reply = buffer.CreateMessage();         // Restore the original message

                var exception = ReadExceptionFromFaultDetail(copy) as CustomServerException;
                if (exception != null)
                {
                    throw exception;
                }
            }
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            return null;
        }

        private static object ReadExceptionFromFaultDetail(Message reply)
        {
            const string detailElementName = "detail";

            using (XmlDictionaryReader reader = reply.GetReaderAtBodyContents())
            {
                // Find <soap:Detail>
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element &&
                        detailElementName.Equals(reader.LocalName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return ReadExceptionFromDetailNode(reader);
                    }
                }
                // Couldn't find it!
                return null;
            }
        }

        private static object ReadExceptionFromDetailNode(XmlDictionaryReader reader)
        {
            // Move to the contents of <soap:Detail>
            if (!reader.Read())
            {
                return null;
            }

            // Return the deserialized fault
            try
            {
                var serializer = new NetDataContractSerializer();
                return serializer.ReadObject(reader);
            }
            catch (SerializationException)
            {
                return null;
            }
        }

        #endregion

        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            if (error is FaultException)
                return false; // Let WCF do normal processing
            
            return true; // Fault message is already generated
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {

            var uniqueKey = UniqueKeyGenerator.Generate();

            //create the custom wcfexception before passing that to client
            var wcfException = new CustomServerException("Unknown Error has occured. Please contact your administrator!", uniqueKey);

            //log the exception
            _logger.Error(uniqueKey, error);

            var user = ServiceSecurityContext.Current;
            MessageFault messageFault = MessageFault.CreateFault(
                new FaultCode("Sender"),
                new FaultReason(wcfException.Message),
                wcfException,
                new NetDataContractSerializer());
            fault = Message.CreateMessage(version, messageFault, null);

        }

        #endregion

        #region IContractBehavior Members

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            ApplyClientBehavior(clientRuntime);
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            ApplyDispatchBehavior(dispatchRuntime.ChannelDispatcher);
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            ApplyClientBehavior(clientRuntime);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            ApplyDispatchBehavior(endpointDispatcher.ChannelDispatcher);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                ApplyDispatchBehavior(dispatcher);
            }
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }

        #endregion

        #region Behavior helpers

        private static void ApplyClientBehavior(System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            foreach (IClientMessageInspector messageInspector in clientRuntime.MessageInspectors)
            {
                if (messageInspector is PassThroughExceptionHandlingBehaviour)
                {
                    return;
                }
            }

            clientRuntime.MessageInspectors.Add(new PassThroughExceptionHandlingBehaviour());
        }

        private static void ApplyDispatchBehavior(System.ServiceModel.Dispatcher.ChannelDispatcher dispatcher)
        {
            // Don't add an error handler if it already exists
            foreach (IErrorHandler errorHandler in dispatcher.ErrorHandlers)
            {
                if (errorHandler is PassThroughExceptionHandlingBehaviour)
                {
                    return;
                }
            }

            dispatcher.ErrorHandlers.Add(new PassThroughExceptionHandlingBehaviour());
        }

        #endregion
    }
}
