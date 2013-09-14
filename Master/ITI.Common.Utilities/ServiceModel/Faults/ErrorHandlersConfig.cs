using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ITI.Common.Utilities.ServiceModel.Faults
{
    /// <summary>
    /// A module which gets the registered error handlers from config section
    /// Developed by Ramy El-Zawahry
    /// </summary>
    public class ErrorHandlersConfig : ConfigurationSection
    {
        [ConfigurationProperty("ErrorHandlerCollection", IsDefaultCollection = false),
    ConfigurationCollection(typeof(Handler), AddItemName = "Handler")]
        public ErrorHandlerCollection ErrorHandlerCollection
        {
            get { return this["ErrorHandlerCollection"] as ErrorHandlerCollection; }
        }
    }

    /// <summary>
    /// Collection of registered error handlers
    /// </summary>
    public class ErrorHandlerCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public Handler this[int index]
        {
            get { return (Handler)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        public Handler this[string key]
        {
            get { return (Handler)BaseGet(key); }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemove(key);
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Handler();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Handler)element).Type;
        }
    }



    /// <summary>
    /// Error handler object which holds the assembly namespace
    /// </summary>
    public class Handler : ConfigurationElement
    {
        [ConfigurationProperty("Type", DefaultValue = "")]
        public string Type
        {
            get { return (string)base["Type"]; }
            set { base["Type"] = value; }
        }
    }


}
