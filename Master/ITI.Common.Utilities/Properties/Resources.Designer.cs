﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITI.Common.Utilities.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ITI.Common.Utilities.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Container not found in IoC configuration.
        /// </summary>
        internal static string exception_ContainerNotFound {
            get {
                return ResourceManager.GetString("exception_ContainerNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to default IocContainer setting not found.
        /// </summary>
        internal static string exception_DefaultIOCSettings {
            get {
                return ResourceManager.GetString("exception_DefaultIOCSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not mapped found for type {0} to type {1}.
        /// </summary>
        internal static string exception_NotMapFoundForTypeAdapter {
            get {
                return ResourceManager.GetString("exception_NotMapFoundForTypeAdapter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Key cannot be null in PerExecutionContextLifetimeManager.
        /// </summary>
        internal static string exception_PerExecutionContextLifetimeManagerKeyCannotBeNull {
            get {
                return ResourceManager.GetString("exception_PerExecutionContextLifetimeManagerKeyCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type is not a fully qualified assembly name.
        /// </summary>
        internal static string exception_RegisterTypeMapConfigurationElementInvalidTypeValue {
            get {
                return ResourceManager.GetString("exception_RegisterTypeMapConfigurationElementInvalidTypeValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid type, expected is RegisterTypesMapConfigurationElement.
        /// </summary>
        internal static string exception_RegisterTypesMapConfigurationInvalidType {
            get {
                return ResourceManager.GetString("exception_RegisterTypesMapConfigurationInvalidType", resourceCulture);
            }
        }
    }
}
