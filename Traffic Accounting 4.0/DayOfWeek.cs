using System;
using System.Collections.Generic;
using System.Text;

namespace Traffic_Accounting
{
    internal static class DayOfWeek
    {
        public enum DayOfWeekValue
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 7
        }

        public static DayOfWeekValue Convert(System.DayOfWeek SystemDayOfWeek)
        {
            switch(SystemDayOfWeek)
            {
                case System.DayOfWeek.Monday:
                    return DayOfWeekValue.Monday;
                case System.DayOfWeek.Tuesday:
                    return DayOfWeekValue.Tuesday;
                case System.DayOfWeek.Wednesday:
                    return DayOfWeekValue.Wednesday;
                case System.DayOfWeek.Thursday:
                    return DayOfWeekValue.Thursday;
                case System.DayOfWeek.Friday:
                    return DayOfWeekValue.Friday;
                case System.DayOfWeek.Saturday:
                    return DayOfWeekValue.Saturday;
                case System.DayOfWeek.Sunday:
                    return DayOfWeekValue.Sunday;
            }
            throw new Exception("Ooops...");
        }
    }
}
