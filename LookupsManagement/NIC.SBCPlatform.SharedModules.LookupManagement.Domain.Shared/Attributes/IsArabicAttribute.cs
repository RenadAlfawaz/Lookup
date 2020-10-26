using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Attributes
{
    public class IsArabicAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value?.ToString(), @"\p{IsArabic}");
        }
    }
}
