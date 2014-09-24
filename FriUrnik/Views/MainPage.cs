using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FriUrnik.Views
{
    class MainPage : TabbedPage
    {
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var columns = await ParseWebsite.GetTableTitles();
            foreach (var column in columns)
            {
                Children.Add(new NavigationPage(new LinksPage(column)) { Title = column });
            }
        }
    }
}
