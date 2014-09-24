using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading;

namespace FriUrnik.Views
{
    class LinksPage : ContentPage
    {
        readonly string columnName;

        public LinksPage(string columName)
        {
            this.columnName = columName;
            var listView = new ListView();
            listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            listView.ItemSelected += HandleItemSelected;
            Content = listView;
        }

        async void HandleItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Link)e.SelectedItem;
            var schedulePage = new SchedulePage();
            await schedulePage.LoadDays(item.Url);
            await Navigation.PushAsync(schedulePage);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var links = await ParseWebsite.GetLinks(columnName);
            (Content as ListView).ItemsSource = links;
        }
    }
}
