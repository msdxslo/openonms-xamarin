using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace FriUrnik.Views
{
    class SchedulePage : CarouselPage
    {
        public async Task LoadDays(string url)
        {
            var weekSchedule = await ParseWebsite.GetSchedule(url);
            foreach (var day in weekSchedule.Days)
            {
                Children.Add(new DayPage(day));
            }
        }
    }
}
