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
    public class UsernameValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool regexValid = false;
            bool usernameInUse = false;
            string validationErrorString = string.Empty;
            bool result = false;
            string validationValue = value.ToString().Trim();
            ValidationResult res = null;
            Regex r = new Regex("^([A-Za-z0-9_-]){4,20}$");

            if (r.IsMatch(validationValue))
            {
                regexValid = true;
            }

            if (regexValid)
            {
                using (var context = new ApplicationDbContext())
                {
                    if (context.ApplicationUser.Where(u => string.Equals(u.UserName, validationValue)).Count() > 0)
                    {
                        usernameInUse = true;
                        validationErrorString = "This username is already in use. Please choose a different one.";
                    }
                }
            }
            else
            {
                validationErrorString = "Your Username must be between 4-20 characters and contain only numbers, letters, underscores, and hyphens.";
            }

            if (!usernameInUse && regexValid)
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
