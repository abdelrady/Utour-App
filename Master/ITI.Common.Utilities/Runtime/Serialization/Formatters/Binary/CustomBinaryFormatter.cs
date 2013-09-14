using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using ITI.Common.Utilities.IO;
using System.Runtime.InteropServices;

namespace ITI.Common.Utilities.Runtime.Serialization.Formatters.Binary
{
    public class CustomBinaryFormatter : IFormatter
    {
        #region -- Local Variables --
        private SerializationBinder m_Binder;
        private StreamingContext m_StreamingContext;
        private ISurrogateSelector m_SurrogateSelector;        
        private static readonly Dictionary<int, Type> m_SerialzableTypes = new Dictionary<int, Type>();
        private static bool m_IsInit = false;
        #endregion        

        #region -- Properties --
        public static Dictionary<int, Type> SerialzableTypes
        {
            get { return CustomBinaryFormatter.m_SerialzableTypes; }
        }

        public ISurrogateSelector SurrogateSelector
        {

            get { return m_SurrogateSelector; }

            set { m_SurrogateSelector = value; }

        }

        public SerializationBinder Binder
        {

            get { return m_Binder; }

            set { m_Binder = value; }

        }

        public StreamingContext Context
        {

            get { return m_StreamingContext; }

            set { m_StreamingContext = value; }

        }

        #endregion

        #region -- Constructor (s) / Destructor --
        public CustomBinaryFormatter()
        {
            if (!m_IsInit)
                Init();
        }
        #endregion

        #region -- Static Methods --
        public static void Init()
        {
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
            var type = typeof(ICustomSerializable);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .ToList()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var t in types)
            {
                m_SerialzableTypes.Add(t.MetadataToken, t);                
            }
            m_IsInit = true;
        }
        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            var type = typeof(ICustomSerializable);
            var types = args.LoadedAssembly
                .GetTypes()                               
                .Where(p => type.IsAssignableFrom(p));

            foreach (var t in types)
            {
                if (!m_SerialzableTypes.Keys.Contains(t.MetadataToken))
                    m_SerialzableTypes.Add(t.MetadataToken, t);
            }
        }
        #endregion

        #region -- Public Methods --
        public object Deserialize(Stream serializationStream)
        {
            ExtendedBinaryReader reader = ExtendedBinaryReader.Create(serializationStream);
            int typeid = reader.ReadInt32();
            Type t = null;
            if (!m_SerialzableTypes.TryGetValue(typeid, out t))
                throw new SerializationException("TypeId " + typeid + " is not a registerred type id");
            object obj = FormatterServices.GetUninitializedObject(t);
            ICustomSerializable deserialize = (ICustomSerializable)obj;
            deserialize.Deserialize(reader);
            return deserialize;

        }

        public void Serialize(Stream serializationStream, object graph)
        {
            ExtendedBinaryWriter writer = ExtendedBinaryWriter.Create(serializationStream);            
            int ObjectKey = graph.GetType().MetadataToken;           
            if(!m_SerialzableTypes.Keys.Contains(ObjectKey))
                throw new SerializationException("TypeId " + ObjectKey + " is not a registerred type id");
            ICustomSerializable c = (ICustomSerializable)graph;
            writer.Write((Int32)ObjectKey);
            c.Serialize(writer);            
        }
        #endregion
        
    }

}
