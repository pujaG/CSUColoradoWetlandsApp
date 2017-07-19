﻿using Plugin.Compass;
using Plugin.Compass.Abstractions;
using Plugin.Geolocator;
using PortableApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PortableApp
{
    public partial class WetlandMapsPage : ViewHelpers
    {
        CustomMap customMap;
        CustomSearchBar searchBar;
        Position savedPosition;
        Geocoder geoCoder;
        Label compassDirection;
        event EventHandler<CompassChangedEventArgs> CompassChanged;

        public WetlandMapsPage()
        {
            // Turn off navigation bar and initialize pageContainer
            NavigationPage.SetHasNavigationBar(this, false);
            AbsoluteLayout pageContainer = ConstructPageContainer();

            // Initialize grid for inner container
            Grid innerContainer = new Grid { Padding = new Thickness(0, Device.OnPlatform(10, 0, 0), 0, 0) };
            innerContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Add header to inner container
            HeaderNavigationOptions navOptions = new HeaderNavigationOptions { titleText = "WETLAND PLANTS", backButtonVisible = true, homeButtonVisible = true };
            Grid navigationBar = ConstructNavigationBar(navOptions);
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            innerContainer.Children.Add(navigationBar, 0, 0);

            searchBar = new CustomSearchBar
            {
                Placeholder = "Search for address or city...",
                FontSize = 12,
                SearchCommand = new Command(() => { })
            };
            searchBar.SearchButtonPressed += ProcessSearch;
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            innerContainer.Children.Add(searchBar, 0, 1);

            // Add map to layout
            customMap = new CustomMap
            {
                MapType = MapType.Satellite,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight
            };

            customMap.ShapeCoordinates.Add(new Position(37.797513, -122.402058));
            customMap.ShapeCoordinates.Add(new Position(37.798433, -122.402256));
            customMap.ShapeCoordinates.Add(new Position(37.798582, -122.401071));
            customMap.ShapeCoordinates.Add(new Position(37.797658, -122.400888));


            // Add location
            GetCurrentLocation();
            var caliPosition = new Position(37.79752, -122.40183); // Latitude, Longitude
            
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(caliPosition, Distance.FromMiles(1)));
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            innerContainer.Children.Add(customMap, 0, 2);

            // Add compass
            if (CrossCompass.IsSupported)
            { 
                compassDirection = new Label { Text = "Direction: ", TextColor = Color.White };
                CrossCompass.Current.CompassChanged += (s, e) =>
                {
                    compassDirection.Text = $"Direction: {e.Heading}";
                };
                CrossCompass.Current.Start();
            }

            // Add FooterBar
            FooterNavigationOptions footerOptions = new FooterNavigationOptions { mapsFooter = true };
            Grid footerBar = ConstructFooterBar(footerOptions);
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });
            innerContainer.Children.Add(footerBar, 0, 3);

            // Add inner container to page container and set as page content
            pageContainer.Children.Add(innerContainer, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            Content = pageContainer;
        }

        private void CustomMap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var location = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            //var cachedLocation = await CrossGeolocator.Current.GetLastKnownLocationAsync();
            savedPosition = new Position(location.Latitude, location.Longitude);
        }

        private async void ProcessSearch(object sender, EventArgs e)
        {
            geoCoder = new Geocoder();
            if (!string.IsNullOrWhiteSpace(searchBar.Text))
            {
                IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(searchBar.Text);
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(approximateLocations.First(), Distance.FromMiles(1)));
            }
        }

    }

    public partial class WetlandMapsKeyPage : ViewHelpers
    {
        public WetlandMapsKeyPage()
        {
            // Turn off navigation bar and initialize pageContainer
            NavigationPage.SetHasNavigationBar(this, false);
            AbsoluteLayout pageContainer = ConstructPageContainer();

            // Initialize grid for inner container
            Grid innerContainer = new Grid
            {
                Padding = new Thickness(20, Device.OnPlatform(30, 20, 20), 20, 20),
                BackgroundColor = Color.FromHex("88000000")
            };
            innerContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Add image of legend
            Image image = new Image { Source = ImageSource.FromResource("PortableApp.Resources.Images.NWI_key.png") };
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            innerContainer.Children.Add(image, 0, 0);

            // Add dismiss button
            Button dismissButton = new Button
            {
                Style = Application.Current.Resources["outlineButton"] as Style,
                Text = "CLOSE",
                BorderRadius = Device.OnPlatform(0, 1, 0)
            };
            dismissButton.Clicked += OnDismissButtonClicked;
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            innerContainer.Children.Add(dismissButton, 0, 1);

            // Add inner container to page container and set as page content
            pageContainer.Children.Add(innerContainer, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            Content = pageContainer;
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }

    public partial class WetlandMapsLegendPage : ViewHelpers
    {
        public WetlandMapsLegendPage()
        {
            // Turn off navigation bar and initialize pageContainer
            NavigationPage.SetHasNavigationBar(this, false);
            AbsoluteLayout pageContainer = ConstructPageContainer();

            // Initialize grid for inner container
            Grid innerContainer = new Grid
            {
                Padding = new Thickness(20, Device.OnPlatform(30, 20, 20), 20, 20),
                BackgroundColor = Color.FromHex("88000000")
            };
            innerContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Add image of legend
            Image image = new Image { Source = ImageSource.FromResource("PortableApp.Resources.Images.NWI_legend.png") };
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            innerContainer.Children.Add(image, 0, 0);

            // Add dismiss button
            Button dismissButton = new Button
            {
                Style = Application.Current.Resources["outlineButton"] as Style,
                Text = "CLOSE",
                BorderRadius = Device.OnPlatform(0, 1, 0)
            };
            dismissButton.Clicked += OnDismissButtonClicked;
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            innerContainer.Children.Add(dismissButton, 0, 1);

            // Add inner container to page container and set as page content
            pageContainer.Children.Add(innerContainer, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            Content = pageContainer;
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }

    public class CustomMap : Map
    {
        public List<WetlandMapOverlay> Overlays { get; set; }
        public List<Position> ShapeCoordinates { get; set; }

        public CustomMap()
        {
            ShapeCoordinates = new List<Position>();
            Overlays = new List<WetlandMapOverlay>(App.WetlandMapOverlayRepo.GetAllWetlandMapOverlays());
        }
    }
}
