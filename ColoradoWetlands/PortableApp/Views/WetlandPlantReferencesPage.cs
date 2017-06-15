﻿using PortableApp.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PortableApp
{
    public partial class WetlandPlantReferencesPage : ViewHelpers
    {

        public WetlandPlantReferencesPage(WetlandPlant plant, ObservableCollection<WetlandPlant> plants)
        {

            // Turn off navigation bar and initialize pageContainer
            NavigationPage.SetHasNavigationBar(this, false);
            AbsoluteLayout pageContainer = ConstructPageContainer();

            // Initialize grid for inner container
            Grid innerContainer = new Grid { Padding = new Thickness(0, Device.OnPlatform(10, 0, 0), 0, 0) };
            innerContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Add header to inner container
            NavigationOptions navOptions = new NavigationOptions { titleText = plant.scinamenoauthorstripped, backButtonVisible = true, nextAndPreviousVisible = true, plant = plant, plants = plants };
            Grid navigationBar = ConstructNavigationBar(navOptions);
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            innerContainer.Children.Add(navigationBar, 0, 0);
            
            ScrollView contentScrollView = new ScrollView {
                BackgroundColor = Color.FromHex("88000000"),
                Padding = new Thickness(20, 5, 20, 5),
                Margin = new Thickness(15, 0, 15, 0)
            };
            StackLayout contentContainer = new StackLayout { Spacing = 10 };

            foreach (WetlandPlantReference reference in plant.References)
            {
                Label referenceLabel = new Label {
                    Text = reference.fullcitation,
                    FontSize = 12,
                    TextColor = Color.White
                };
                contentContainer.Children.Add(referenceLabel);
            }
                        
            contentScrollView.Content = contentContainer;
            innerContainer.RowDefinitions.Add(new RowDefinition { });
            innerContainer.Children.Add(contentScrollView, 0, 1);

            // Add inner container to page container and set as page content
            pageContainer.Children.Add(innerContainer, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            Content = pageContainer;
        }

    }
}