using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.Xml;

namespace ITI.Common.Utilities.Runtime.Serialization.Encoders.GZip
{
    public class GZipMessageEncodingBindingElementImporter : IPolicyImportExtension
    {
        public GZipMessageEncodingBindingElementImporter()
        {
        }

        void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
        {
            if (importer == null)
            {
                throw new ArgumentNullException("importer");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            ICollection<XmlElement> assertions = context.GetBindingAssertions();
            foreach (XmlElement assertion in assertions)
            {
                if ((assertion.NamespaceURI == GZipMessageEncodingPolicyConstants.GZipEncodingNamespace) &&
                    (assertion.LocalName == GZipMessageEncodingPolicyConstants.GZipEncodingName)
                    )
                {
                    assertions.Remove(assertion);
                    context.BindingElements.Add(new GZipMessageEncodingBindingElement());
                    break;
                }
            }
        }
    }
}
