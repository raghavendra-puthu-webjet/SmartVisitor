using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartVisitor1.Views;
using Xamarin.Forms;

namespace SmartVisitor1
{
    public class App : Application
    {
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Company { get; set; }

        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new LandingVIew());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
