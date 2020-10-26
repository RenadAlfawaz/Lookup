using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Attributes
{
    public class IsEnglishAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value?.ToString(), @"[A-Za-z0-9.,-=+(){}\[\]\\]");
        }
    }
}
