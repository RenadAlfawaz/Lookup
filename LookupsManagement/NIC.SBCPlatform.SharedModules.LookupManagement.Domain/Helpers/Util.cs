using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Helpers
{
    public static class Util
    {
        public static bool IsArabic(string str)
        {
            return Regex.IsMatch(str, @"\p{IsArabic}");
        }

        public static bool IsEnglish(string str)
        {
            var regex = new Regex(@"[A-Za-z0-9 .,-=+(){}\[\]\\]");
            var matches = regex.Matches(str);

            if (matches.Count.Equals(str.Length))
                return true;
            else
                return false;
        } 
    }
}
