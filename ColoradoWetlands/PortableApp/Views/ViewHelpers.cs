﻿using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PortableApp
{
    public class ViewHelpers : ContentPage
    {

        public ExternalDBConnection externalConnection = new ExternalDBConnection();

        //
        // VIEWS
        //

        // Construct Page Container as an AbsoluteLayout with a background image
        public AbsoluteLayout ConstructPageContainer()
        {
            AbsoluteLayout pageContainer = new AbsoluteLayout { BackgroundColor = Color.Black };
            Image backgroundImage = new Image {
                Source = ImageSource.FromResource("PortableApp.Resources.Images.background.jpg"),
                Aspect = Aspect.AspectFill,
                Opacity = 0.7
            };
            pageContainer.Children.Add(backgroundImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            return pageContainer;
        }

        // Construct Navigation Bar
        public Grid ConstructNavigationBar(string titleText, bool backButtonVisible = true, bool homeButtonVisible = true, bool logoVisible = false)
        {
            Grid gridLayout = new Grid { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });

            // Construct back button and add gesture recognizer
            if (backButtonVisible)
            {
                Image backImage = new Image
                {
                    Source = ImageSource.FromResource("PortableApp.Resources.Icons.back_arrow.png"),
                    HeightRequest = 20,
                    WidthRequest = 20,
                    Margin = new Thickness(0, 15, 0, 15)
                };
                var backGestureRecognizer = new TapGestureRecognizer();
                backGestureRecognizer.Tapped += async (sender, e) =>
                {
                    backImage.Opacity = .5;
                    await Task.Delay(200);
                    backImage.Opacity = 1;
                    await Navigation.PopAsync();
                };
                backImage.GestureRecognizers.Add(backGestureRecognizer);
                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridLayout.Children.Add(backImage, 0, 0);
            }
            else
            {
                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0) });
            }

            // Construct title content section
            StackLayout titleContent = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
            Image logo = new Image { Source = ImageSource.FromResource("PortableApp.Resources.Icons.Co_Logo_40.png"), IsVisible = logoVisible };
            Label title = new Label { Text = titleText, FontFamily = Device.OnPlatform("Montserrat-Medium", "Montserrat-Medium.ttf#Montserrat-Medium", null), TextColor = Color.White, FontSize = 18, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            titleContent.Children.Add(logo);
            titleContent.Children.Add(title);
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
            gridLayout.Children.Add(titleContent, 1, 0);

            // Construct home button and add gesture recognizer
            if (homeButtonVisible)
            {
                Image homeImage = new Image
                {
                    Source = ImageSource.FromResource("PortableApp.Resources.Icons.home_icon.png"),
                    HeightRequest = 20,
                    WidthRequest = 20,
                    Margin = new Thickness(0, 15, 0, 15)
                };
                var homeImageGestureRecognizer = new TapGestureRecognizer();
                homeImageGestureRecognizer.Tapped += async (sender, e) =>
                {
                    homeImage.Opacity = .5;
                    await Task.Delay(200);
                    homeImage.Opacity = 1;
                    await Navigation.PopToRootAsync();
                    await Navigation.PushAsync(new MainPage());
                };
                homeImage.GestureRecognizers.Add(homeImageGestureRecognizer);
                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridLayout.Children.Add(homeImage, 2, 0);
            }
            else
            {
                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0) });
            }
            
            return gridLayout;
        }

        public WebView HTMLProcessor(string location)
        {
            // Generate WebView container
            var browser = new TransparentWebView();
            //var pdfBrowser = new CustomWebView { Uri = location, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            string htmlText;

            // Get file locally unless the location is a web address
            if (location.Contains("http"))
            {
                htmlText = location;
                browser.Source = htmlText;
            }
            else if (!location.Contains(".pdf"))
            {
                // Get file from PCL--in order for HTML files to be automatically pulled from the PCL, they need to be in a Views/HTML folder
                var assembly = typeof(HTMLPage).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("PortableApp.Views.HTML." + location);
                htmlText = "";
                using (var reader = new System.IO.StreamReader(stream)) { htmlText = reader.ReadToEnd(); }
                var htmlSource = new HtmlWebViewSource();
                htmlSource.Html = htmlText;
                browser.Source = htmlSource;
            }
            return browser;
        }

        protected async void ChangeButtonColor(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundColor = Color.FromHex("BB105456");
            await Task.Delay(100);
            button.BackgroundColor = Color.FromHex("66000000");
        }

        //
        // NAVIGATION
        //

        public async void ToIntroduction(object sender, EventArgs e)
        {
            ChangeButtonColor(sender, e);
            await Navigation.PushAsync(new HTMLPage("Introduction.html", "INTRODUCTION"));
        }

        public async void ToWetlandPlants(object sender, EventArgs e)
        {
            ChangeButtonColor(sender, e);
            await Navigation.PushAsync(new WetlandPlantsPage());
        }

        public async void ToWetlandMaps(object sender, EventArgs e)
        {
            ChangeButtonColor(sender, e);
            await Navigation.PushAsync(new WetlandPlantsPage());
        }

        public async void ToWetlandTypes(object sender, EventArgs e)
        {
            ChangeButtonColor(sender, e);
            await Navigation.PushAsync(new WetlandTypesPage());
        }

        public async void ToAcknowledgements(object sender, EventArgs e)
        {
            ChangeButtonColor(sender, e);
            await Navigation.PushAsync(new HTMLPage("Acknowledgements.html", "ACKNOWLEDGEMENTS"));
        }

    }
}
