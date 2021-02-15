using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Projekt.Helpers
{
    public static class CalculateAgeHelper
    {
        public static string CalculateAge(DateTime postDate)
        {
            string returnAge = string.Empty;
            DateTime today = DateTime.Now;
            returnAge = (today - postDate).Days.ToString();
            if ((today - postDate).Days < 1)
                return "Today";
            else
                return returnAge + " days ago";
        }

    }
}
