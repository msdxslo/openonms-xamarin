using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using FriUrnik.Views;
using System.Threading.Tasks;

namespace FriUrnik
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new MainPage();
        }
    }
}