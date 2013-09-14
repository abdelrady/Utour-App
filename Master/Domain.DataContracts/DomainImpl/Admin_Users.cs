using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain.DataContracts
{
    [MetadataType(typeof(Admin_Users.Admin_UsersMetaData))]
    public partial class Admin_Users
    {
        internal sealed class Admin_UsersMetaData
        {
            [Required(ErrorMessageResourceName = "USerNameRequiredMessage",
                ErrorMessageResourceType = typeof (DoaminContractResource))]
            public object UserName { get; set; }

            [Required(ErrorMessageResourceName = "PasswordRequiredMessage",
                ErrorMessageResourceType = typeof (DoaminContractResource))]
            public object Password { get; set; }
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
                var context = new ValidationContext(this, serviceProvider: null, items: null) { MemberName = columnName };
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
