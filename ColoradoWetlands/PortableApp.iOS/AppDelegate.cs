﻿using Foundation;
using UIKit;
using SQLite.Net.Platform.XamarinIOS;
using CarouselView.FormsPlugin.iOS;

namespace PortableApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            CarouselViewRenderer.Init();

            string dbPath = FileAccessHelper.GetLocalFilePath("db.db3");
            var platform = new SQLitePlatformIOS();
            LoadApplication(new PortableApp.App(platform, dbPath));

            return base.FinishedLaunching(app, options);
        }
    }
}
