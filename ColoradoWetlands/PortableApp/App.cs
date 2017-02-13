﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PortableApp
{
    public class App : Application
    {
        public static PlantRepository PlantRepo { get; private set; }
        public static PlantTypeRepository PlantTypeRepo { get; private set; }

        public App(string dbPath)
        {
            PlantRepo = new PlantRepository(dbPath);
            PlantTypeRepo = new PlantTypeRepository(dbPath);
            this.MainPage = new NavigationPage (new MainPage(dbPath));
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
