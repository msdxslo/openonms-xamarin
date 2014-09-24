using System;
using Xamarin.Forms;
using FriUrnik.Models;

namespace FriUrnik.Views
{
    class ClassView:StackLayout
    {
        public ClassView(ClassInfo classInfo)
        {
            Children.Add(new Label(){ Text = classInfo.Time });
            Children.Add(new Label(){ Text = classInfo.Name });
            Children.Add(new Label(){ Text = classInfo.ClassRoom });
            var lecturesView = new StackLayout();
            lecturesView.IsVisible = false;
            foreach (var lecturer in classInfo.Lecturers)
            {
                lecturesView.Children.Add(new Label(){ Text = lecturer });
            }
            Children.Add(lecturesView);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                lecturesView.IsVisible = !lecturesView.IsVisible;
            };
            GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}

