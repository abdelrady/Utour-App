using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Threading.Tasks;


namespace ITI.Common.Utilities.ServiceModel.Logging
{
    public class WcfMessageLoggerConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("ConnectionString", DefaultValue = "", IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)base["ConnectionString"]; }
            set { base["ConnectionString"] = value; }
        }

        [ConfigurationProperty("MaxMessageSize", DefaultValue = "104857600", IsRequired = true)]
        public int MaxMessageSize
        {
            get { return int.Parse(base["MaxMessageSize"].ToString()); }
            set { base["MaxMessageSize"] = value; }
        }

        [ConfigurationProperty("LogRequestOnly", DefaultValue = "false", IsRequired = true)]
        public bool LogRequestOnly
        {
            get { return bool.Parse(base["LogRequestOnly"].ToString()); }
            set { base["LogRequestOnly"] = value; }
        }

        [ConfigurationProperty("Enabled", DefaultValue = "true", IsRequired = true)]
        public bool Enabled
        {
            get { return bool.Parse(base["Enabled"].ToString()); }
            set { base["Enabled"] = value; }
        }

        [ConfigurationProperty("EventLogSource", DefaultValue = "WCF Message Logger", IsRequired = true)]
        public string EventLogSource
        {
            get { return (string)base["EventLogSource"]; }
            set { base["EventLogSource"] = value; }
        }
    }

    public class WcfMessageLogger : Attribute, IDispatchMessageInspector, IServiceBehavior
    {
        #region -- Local Varaibles --
        private readonly string m_ConnectionString;
        private readonly int m_MaxMessageSize;
        private readonly bool m_LogRequestOnly;
        private readonly string m_EventLogSource;
        private readonly bool m_IsEnabled;
        #endregion

        #region -- Properties --
        public string ConnectionString
        {
            get { return m_ConnectionString; }
        }
        public int MaxMessageSize
        {
            get { return m_MaxMessageSize; }
        }
        public bool LogRequestOnly
        {
            get { return m_LogRequestOnly; }
        }
        public string EventLogSource
        {
            get { return m_EventLogSource; }
        }
        public bool IsEnabled
        {
            get { return m_IsEnabled; }
        } 
        #endregion

        #region -- Constructor --

        public WcfMessageLogger(string ConfigSectionName)
        {
            WcfMessageLoggerConfigurationSection config = ConfigurationManager.GetSection(ConfigSectionName) as WcfMessageLoggerConfigurationSection;
            if (config != null)
            {
                this.m_IsEnabled = config.Enabled;
                this.m_ConnectionString = config.ConnectionString;
                this.m_MaxMessageSize = config.MaxMessageSize;
                this.m_LogRequestOnly = config.LogRequestOnly;
                this.m_EventLogSource = config.EventLogSource;
            }
            else
                throw new ConfigurationErrorsException("Couldn't find WcfMessageLoggerConfigurationSection");
        }

        public WcfMessageLogger(bool Enabled, string LogConnectionString, int MaxMessageSize, bool LogRequestOnly, string EventLogName)
        {
            this.m_IsEnabled = Enabled;
            this.m_ConnectionString = LogConnectionString;
            this.m_MaxMessageSize = MaxMessageSize;
            this.m_LogRequestOnly = LogRequestOnly;
            this.m_EventLogSource = EventLogName;
        }
        #endregion

        #region -- IDispatchMessageInspector --

        public object AfterReceiveRequest(ref Message request, IClientChannel channel,
            InstanceContext instanceContext)
        {
            if (IsEnabled)
            {
                OperationContext context = OperationContext.Current;
                MessageBuffer buffer = request.CreateBufferedCopy(int.MaxValue);
                request = buffer.CreateMessage();
                if (context != null && buffer.BufferSize <= this.MaxMessageSize)
                {
                    try
                    {
                        Message tmpMesage = buffer.CreateMessage();
                        Process CurrentProcess = Process.GetCurrentProcess();                        
                        WindowsIdentity SourceIdentity = WindowsIdentity.GetCurrent();
                        MessageProperties messageProperties = context.IncomingMessageProperties;
                        RemoteEndpointMessageProperty endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                        IIdentity ClientIdentity = null;
                        if (context.ServiceSecurityContext != null)
                            ClientIdentity = context.ServiceSecurityContext.PrimaryIdentity;
                        WcfEvent wcfEvent = new WcfEvent()
                        {
                            MessageID = Guid.NewGuid(),
                            CorrelationID = Guid.Empty,
                            ProcessID = CurrentProcess.Id,
                            ThreadID = Thread.CurrentThread.ManagedThreadId,
                            TimeCreated = DateTime.Now,
                            ServiceName = context.Host.Description.Name,
                            ServiceMachineName = Environment.MachineName,
                            ServiceUri = context.Channel.LocalAddress.ToString(),
                            ServiceIP = context.Channel.LocalAddress.Uri.Host,
                            ServicePort = context.Channel.LocalAddress.Uri.Port,
                            ServiceIdentity = SourceIdentity.Name,
                            ServiceAuthenticationType = SourceIdentity.AuthenticationType,
                            ClientIP = endpointProperty.Address,
                            ClientPort = endpointProperty.Port,
                            ClientIdentity = ClientIdentity == null ? "Anonymous" : ClientIdentity.Name,
                            ClientAuthenticationType = ClientIdentity == null ? "None" : ClientIdentity.AuthenticationType,
                            Action = request.Headers.Action,
                            Parameters = tmpMesage.GetReaderAtBodyContents().ReadOuterXml(),
                            Response = null,
                            Misc = null,
                            IsFault = request.IsFault
                        };
                        return wcfEvent;
                    }
                    catch (Exception ex)
                    {
                        if (!System.Diagnostics.EventLog.SourceExists(this.EventLogSource))
                        {
                            System.Diagnostics.EventLog.CreateEventSource(this.EventLogSource, "WcfMessageLogger");
                        }
                        System.Diagnostics.EventLog.WriteEntry(this.EventLogSource, ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
                    }
                }
            }
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (correlationState != null && correlationState.GetType() == typeof(WcfEvent))
            {
                WcfEvent wcfEvent = (WcfEvent)correlationState;
                wcfEvent.IsFault = reply.IsFault;
                if (this.LogRequestOnly && wcfEvent.Parameters.Length <= this.MaxMessageSize)
                    Task.Factory.StartNew(() => AddWcfEvent((WcfEvent)correlationState));
                else
                {
                    MessageBuffer buffer = reply.CreateBufferedCopy(int.MaxValue);
                    reply = buffer.CreateMessage();
                    if (buffer.BufferSize + wcfEvent.Parameters.Length <= this.MaxMessageSize)
                    {
                        wcfEvent.Response = buffer.CreateMessage().GetReaderAtBodyContents().ReadInnerXml();
                        Task.Factory.StartNew(() => AddWcfEvent(wcfEvent));
                    }
                }
            }
        }

        #endregion

        #region -- IServiceBehavior --
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {

            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in dispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(this);
                }
            }
        }
        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }
        public void Validate(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
        }
        #endregion

        #region -- Private Methods --
        private void AddWcfEvent(WcfEvent wcfEvent)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.Connection = con;
                    objCommand.CommandText = "AddWcfEvent";
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddRange(new SqlParameter[]{
                    new SqlParameter("@MessageID",wcfEvent.MessageID),
                    new SqlParameter("@CorrelationID",wcfEvent.CorrelationID),
                    new SqlParameter("@ProcessID",wcfEvent.ProcessID),
                    new SqlParameter("@ThreadID",wcfEvent.ThreadID),
                    new SqlParameter("@TimeCreated",wcfEvent.TimeCreated),
                    new SqlParameter("@ServiceName",wcfEvent.ServiceName),
                    new SqlParameter("@ServiceMachineName",wcfEvent.ServiceMachineName),
                    new SqlParameter("@ServiceUri",wcfEvent.ServiceUri),
                    new SqlParameter("@ServiceIP",wcfEvent.ServiceIP),
                    new SqlParameter("@ServicePort",wcfEvent.ServicePort),
                    new SqlParameter("@ServiceIdentity",wcfEvent.ServiceIdentity),
                    new SqlParameter("@ServiceAuthenticationType",wcfEvent.ServiceAuthenticationType),
                    new SqlParameter("@ClientIP",wcfEvent.ClientIP),
                    new SqlParameter("@ClientPort",wcfEvent.ClientPort),
                    new SqlParameter("@ClientIdentity",wcfEvent.ClientIdentity),
                    new SqlParameter("@ClientAuthenticationType",wcfEvent.ClientAuthenticationType),
                    new SqlParameter("@Action",wcfEvent.Action),
                    new SqlParameter("@IsFault",wcfEvent.IsFault)});

                    if (string.IsNullOrEmpty(wcfEvent.Parameters))
                        objCommand.Parameters.Add(new SqlParameter("@Parameters", DBNull.Value));
                    else
                        objCommand.Parameters.Add(new SqlParameter("@Parameters", wcfEvent.Parameters));

                    if (string.IsNullOrEmpty(wcfEvent.Response))
                        objCommand.Parameters.Add(new SqlParameter("@Response", DBNull.Value));
                    else
                        objCommand.Parameters.Add(new SqlParameter("@Response", wcfEvent.Response));

                    if (string.IsNullOrEmpty(wcfEvent.Misc))
                        objCommand.Parameters.Add(new SqlParameter("@Misc", DBNull.Value));
                    else
                        objCommand.Parameters.Add(new SqlParameter("@Misc", wcfEvent.Misc));

                    con.Open();
                    objCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (!System.Diagnostics.EventLog.SourceExists(this.EventLogSource))
                {
                    System.Diagnostics.EventLog.CreateEventSource(this.EventLogSource, "WcfMessageLogger");
                }
                System.Diagnostics.EventLog.WriteEntry(this.EventLogSource, ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
        }
        #endregion

        #region -- Inner Classes --
        private class WcfEvent
        {
            public long EventID { get; set; }
            public System.Guid MessageID { get; set; }
            public System.Guid CorrelationID { get; set; }
            public int ThreadID { get; set; }
            public int ProcessID { get; set; }
            public System.DateTime TimeCreated { get; set; }
            public string ServiceName { get; set; }
            public string ServiceMachineName { get; set; }
            public string ServiceUri { get; set; }
            public string ServiceIP { get; set; }
            public int ServicePort { get; set; }
            public string ServiceIdentity { get; set; }
            public string ServiceAuthenticationType { get; set; }
            public string ClientIP { get; set; }
            public int ClientPort { get; set; }
            public string ClientIdentity { get; set; }
            public string ClientAuthenticationType { get; set; }
            public string Action { get; set; }
            public string Parameters { get; set; }
            public string Response { get; set; }
            public string Misc { get; set; }
            public Nullable<bool> IsFault { get; set; }
        }
        #endregion


    }

    public class WcfMessageLoggerExtension : BehaviorExtensionElement
    {
        [ConfigurationProperty("ConnectionString", DefaultValue = "", IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)base["ConnectionString"]; }
            set { base["ConnectionString"] = value; }
        }

        [ConfigurationProperty("MaxMessageSize", DefaultValue = "104857600", IsRequired = true)]
        public int MaxMessageSize
        {
            get { return int.Parse(base["MaxMessageSize"].ToString()); }
            set { base["MaxMessageSize"] = value; }
        }

        [ConfigurationProperty("LogRequestOnly", DefaultValue = "false", IsRequired = true)]
        public bool LogRequestOnly
        {
            get { return bool.Parse(base["LogRequestOnly"].ToString()); }
            set { base["LogRequestOnly"] = value; }
        }

        [ConfigurationProperty("Enabled", DefaultValue = "true", IsRequired = true)]
        public bool Enabled
        {
            get { return bool.Parse(base["Enabled"].ToString()); }
            set { base["Enabled"] = value; }
        }

        [ConfigurationProperty("EventLogSource", DefaultValue = "WCF Message Logger", IsRequired = true)]
        public string EventLogSource
        {
            get { return (string)base["EventLogSource"]; }
            set { base["EventLogSource"] = value; }
        }
        
        protected override object CreateBehavior()
        {
            return new WcfMessageLogger(Enabled,ConnectionString, MaxMessageSize, LogRequestOnly, EventLogSource);
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(WcfMessageLogger);
            }
        }

        
    }
}
