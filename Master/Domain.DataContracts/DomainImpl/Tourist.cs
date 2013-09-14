using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.DataContracts
{
    [MetadataType(typeof (TouristMetaData))]
    public partial class Tourist
    {
        private class EmailAttribute : RegularExpressionAttribute
        {
            public EmailAttribute()
                : base(GetRegex())
            {
            }

            private static string GetRegex()
            {
                // TODO: Go off and get your RegEx here
                return @"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$";
            }
        }

        internal sealed class TouristMetaData
        {
            [Required(ErrorMessageResourceName = "USerNameRequiredMessage",
                ErrorMessageResourceType = typeof(DoaminContractResource))]
            public object UserName { get; set; }

            [Required(ErrorMessageResourceName = "PasswordRequiredMessage",
                ErrorMessageResourceType = typeof(DoaminContractResource))]
            public object Password { get; set; }

            [Required(ErrorMessageResourceName = "First_NameRequiredMessage",
                ErrorMessageResourceType = typeof(DoaminContractResource))]
            public object First_Name { get; set; }

            [Required(ErrorMessageResourceName = "Last_NameRequiredMessage",
                ErrorMessageResourceType = typeof(DoaminContractResource))]
            public object Last_Name { get; set; }
            
            public object Gender { get; set; }
            public object Nationality { get; set; }


            [Required(ErrorMessageResourceName = "EmailRequiredMessage",
                ErrorMessageResourceType = typeof(DoaminContractResource))]
            [Email(ErrorMessageResourceName = "EmailErrorMessage",
                ErrorMessageResourceType = typeof(DoaminContractResource))]
            public object Email { get; set; }

            [Required(ErrorMessageResourceName = "Preferred_LanguageRequiredMessage",
                ErrorMessageResourceType = typeof(DoaminContractResource))]
            public object Preferred_Language { get; set; }

            
            //[Range(199000, long.MaxValue)]
            //[Required]
            //[Range(1, int.MaxValue, ErrorMessage = "Client ID out of range")]
        }

        public string Error
        {
            get
            {
                TypeDescriptor.AddProviderTransparent(
                    new AssociatedMetadataTypeTypeDescriptionProvider(this.GetType()), this.GetType());
                StringBuilder b = new StringBuilder();
                var context = new ValidationContext(this, serviceProvider: null, items: null);
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var isValid = Validator.TryValidateObject(this, context, results, true);
                if (!isValid)
                {
                    foreach (var validationResult in results)
                    {
                        b.AppendLine(validationResult.ErrorMessage);
                    }
                }
                return b.ToString();
            }
        }

        public string this[string columnName]
        {
            get
            {
                TypeDescriptor.AddProviderTransparent(
                    new AssociatedMetadataTypeTypeDescriptionProvider(this.GetType()), this.GetType());
                StringBuilder b = new StringBuilder();
                var context = new ValidationContext(this, serviceProvider: null, items: null) {MemberName = columnName};
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                foreach (var itm in this.GetType().GetProperties())
                {
                    if (itm.Name == columnName)
                    {
                        var isValid = Validator.TryValidateProperty(itm.GetValue(this, null), context, results);
                        if (!isValid)
                        {
                            foreach (var validationResult in results)
                            {
                                b.AppendLine(validationResult.ErrorMessage);
                            }
                        }
                    }
                }
                //if (!this.CheckIsSuspend())
                //    b.Append(DoaminContractResource.SuspensionError);
                return b.ToString();
            }
        }

        public bool IsValid
        {
            get { return String.IsNullOrEmpty(this.Error); }
        }
    }
}
