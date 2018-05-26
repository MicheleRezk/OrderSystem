using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Application
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Check if today is Sunday or Public Holiday in Netherlands then return true, else return false
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsTodaySundayOrPublicHoliday(this DateTime date)
        {
            return DateSystem.IsPublicHoliday(date, CountryCode.NL) 
                    ||
                    date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
