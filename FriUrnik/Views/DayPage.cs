using System;
using Xamarin.Forms;
using FriUrnik.Models;

namespace FriUrnik.Views
{
    public class DayPage : ContentPage
    {
        public DayPage(DaySchedule daySchedule)
        {
            var scrollView = new ScrollView();
            var mainStackLayout = new StackLayout();
            mainStackLayout.Children.Add(new Label(){ Text = daySchedule.DayName, Font = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold) });
            foreach (var classInfo in daySchedule.Classes)
            {
                mainStackLayout.Children.Add(new ClassView(classInfo));
                //mainStackLayout.Children.Add(new BoxView(){ HeightRequest = 2, Color = Color.Red });
            }
            scrollView.Content = mainStackLayout;
            Content = scrollView;
        }
    }
}

