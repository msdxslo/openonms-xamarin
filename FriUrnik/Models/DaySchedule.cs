using System;
using System.Collections.Generic;
using System.Text;

namespace FriUrnik.Models
{
    public class DaySchedule
    {
        public DaySchedule(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    DayName = "Ponedeljek";
                    break;
                case DayOfWeek.Tuesday:
                    DayName = "Torek";
                    break;
                case DayOfWeek.Wednesday:
                    DayName = "Sreda";
                    break;
                case DayOfWeek.Thursday:
                    DayName = "Četrtek";
                    break;
                case DayOfWeek.Friday:
                    DayName = "Petek";
                    break;
            }
            Classes = new List<ClassInfo>();
        }

        public string DayName { get; set; }

        public List<ClassInfo> Classes { get; set; }
    }
}
