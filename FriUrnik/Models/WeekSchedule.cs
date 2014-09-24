using System;
using System.Collections.Generic;
using System.Text;

namespace FriUrnik.Models
{
    public class WeekSchedule
    {
        public DaySchedule[] Days { get; set; }

        public WeekSchedule()
        {
            Days = new DaySchedule[5];
            for (int i = 0; i < 5; i++)
                Days[i] = new DaySchedule((DayOfWeek)(i + 1));
        }
    }
}
