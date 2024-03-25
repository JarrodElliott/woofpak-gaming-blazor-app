using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WoofpakGamingSiteServerApp.Data.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property |
    AttributeTargets.Field, AllowMultiple = false)]
    public class EmailAddressValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool regexValid = false;
            bool emailInUse = false;
            string validationErrorString = string.Empty;
            bool result = false;
            string validationValue = value.ToString().Trim();
            ValidationResult res = null;
            Regex r = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");

            if (r.IsMatch(validationValue))
            {
                regexValid = true;
            }

            if (regexValid)
            {
                using (var context = new ApplicationDbContext())
                {
                    if (context.ApplicationUser.Where(u => string.Equals(u.Email, validationValue)).Count() > 0)
                    {
                        emailInUse = true;
                        validationErrorString = "This email address is already in use. If this is you, sign in!";
                    }
                }
            }
            else
            {
                validationErrorString = "Please enter a valid email address!";
            }

            if (!emailInUse && regexValid)
            {
                res = ValidationResult.Success;
            }
            else
            {
                res = new ValidationResult(validationErrorString);
            }

            return res;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }
    }
}
