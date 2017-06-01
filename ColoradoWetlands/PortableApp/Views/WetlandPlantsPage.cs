﻿using PortableApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PortableApp
{
    public partial class WetlandPlantsPage : ViewHelpers
    {
        ListView wetlandPlantsList;
        ObservableCollection<WetlandPlant> plants;
        Dictionary<string, string> sortOptions = new Dictionary<string, string> { { "Scientific Name", "scinamenoauthor" }, { "Common Name", "commonname" }, { "Family", "family" } };
        Picker sortPicker = new Picker();
        Button sortButton = new Button { Style = Application.Current.Resources["semiTransparentPlantButton"] as Style, Text = "Scientific Name", BorderRadius = Device.OnPlatform(0, 1, 0) };
        Button sortDirection = new Button { Style = Application.Current.Resources["semiTransparentPlantButton"] as Style, Text = "\u25BC", BorderRadius = Device.OnPlatform(0, 1, 0) };

        protected override async void OnAppearing()
        {
            plants = new ObservableCollection<WetlandPlant>(App.WetlandPlantRepo.GetAllWetlandPlants());

            // get and compare local data updated date and that of the server, sending to download page if date on server is newer
            WetlandSetting datePlantDataUpdatedLocally = await App.WetlandSettingsRepo.GetSettingAsync("DatePlantsDownloaded");
            if (datePlantDataUpdatedLocally != null)
            {
                //WetlandSetting datePlantDataUpdatedOnServer = await externalConnection.GetDateUpdatedDataOnServer();
                //if (datePlantDataUpdatedLocally.valuetimestamp < datePlantDataUpdatedOnServer.valuetimestamp || plants.Count == 0)
                //{
                //    await Navigation.PushAsync(new DownloadWetlandPlantsPage());
                //}
            }
            else
            {
                await Navigation.PushAsync(new DownloadWetlandPlantsPage());
            }

            wetlandPlantsList.ItemsSource = plants;
            base.OnAppearing();
        }

        public WetlandPlantsPage()
        {
            // Turn off navigation bar and initialize pageContainer
            NavigationPage.SetHasNavigationBar(this, false);
            AbsoluteLayout pageContainer = ConstructPageContainer();

            // Initialize grid for inner container
            Grid innerContainer = new Grid { Padding = new Thickness(0, Device.OnPlatform(10, 0, 0), 0, 0) };
            innerContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Add header to inner container
            NavigationOptions navOptions = new NavigationOptions { titleText = "WETLAND PLANTS", backButtonVisible = true, homeButtonVisible = true };
            Grid navigationBar = ConstructNavigationBar(navOptions);
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            innerContainer.Children.Add(navigationBar, 0, 0);

            // Add button group grid
            Grid buttonGroup = new Grid();
            buttonGroup.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            buttonGroup.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            buttonGroup.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                // Add search button
                Button search = new Button {
                    Style = Application.Current.Resources["semiTransparentPlantButton"] as Style,
                    Text = "Search",
                    BorderRadius = Device.OnPlatform(0, 1, 0),
                    Margin = new Thickness(10, 0, 10, 0)
                };
                buttonGroup.Children.Add(search, 0, 0);

                search.Clicked += OpenFilterList;

                // Add sort container
                Grid sortContainer = new Grid { ColumnSpacing = 0 };
                sortContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.7, GridUnitType.Star) });
                sortContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
                sortContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) });
                sortContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

                sortButton.Clicked += SortPickerTapped;
                sortContainer.Children.Add(sortButton, 0, 0);

                foreach (string option in sortOptions.Keys) { sortPicker.Items.Add(option); }
                sortPicker.IsVisible = false;
                sortPicker.SelectedIndex = 0;
                if (Device.OS == TargetPlatform.iOS)
                    sortPicker.Unfocused += SortOnUnfocused;
                else
                    sortPicker.SelectedIndexChanged += SortItems;

                sortContainer.Children.Add(sortPicker, 0, 0);

                sortDirection.Clicked += ChangeSortDirection;
                sortContainer.Children.Add(sortDirection, 1, 0);

                buttonGroup.Children.Add(sortContainer, 1, 0);

            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            innerContainer.Children.Add(buttonGroup, 0, 1);

            wetlandPlantsList = new ListView { BackgroundColor = Color.Transparent, RowHeight = 100 };
            wetlandPlantsList.ItemTemplate = new DataTemplate(typeof(WetlandPlantsItemTemplate));
            wetlandPlantsList.ItemSelected += OnItemSelected;
            innerContainer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            innerContainer.Children.Add(wetlandPlantsList, 0, 2);

            // Add inner container to page container and set as page content
            pageContainer.Children.Add(innerContainer, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            Content = pageContainer;
        }

        async private void OpenFilterList(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new WetlandPlantsFilterPage());
        }

        private void SortPickerTapped(object sender, EventArgs e)
        {
            sortPicker.Focus();
        }

        private void SortOnUnfocused(object sender, FocusEventArgs e)
        {
            SortItems(sender, e);
        }

        private void SortItems(object sender, EventArgs e)
        {
            sortButton.Text = sortPicker.Items[sortPicker.SelectedIndex];
            wetlandPlantsList.ItemsSource = null;
            if (sortButton.Text == "Scientific Name")
                plants.Sort(i => i.scinamenoauthor, sortDirection.Text);
            else if (sortButton.Text == "Common Name")
                plants.Sort(i => i.commonname, sortDirection.Text);
            else if (sortButton.Text == "Family")
                plants.Sort(i => i.family, sortDirection.Text);

            wetlandPlantsList.ItemsSource = plants;
        }

        private void ChangeSortDirection(object sender, EventArgs e)
        {
            if (sortDirection.Text == "\u25BC")
            {
                sortDirection.Text = "\u25B2";
            }
            else
            {
                sortDirection.Text = "\u25BC";
            }
            SortItems(sender, e);
        }

        public List<WetlandPlant> WetlandPlantsList()
        {

            var wetlandPlants = new List<WetlandPlant>();
            wetlandPlants.Add(new WetlandPlant
            {
                scinameauthor = "ACER NEGUNDO",
                commonname = "Boxelder",
                family = "Aceraceae",
                topimgtopimg = "ACNE2_3.jpg"
            });
            wetlandPlants.Add(new WetlandPlant
            {
                scinameauthor = "ACONITUM COLUMBIANUM",
                commonname = "Columbian Monkshood",
                family = "Ranunculaceae (Helleboraceae)",
                topimgtopimg = "ACCO4_1.jpg"
            });
            wetlandPlants.Add(new WetlandPlant
            {
                scinameauthor = "ACORUS CALAMUS",
                commonname = "Calamus",
                family = "Acoraceae",
                topimgtopimg = "ACCA4_2.jpg"
            });
            wetlandPlants.Add(new WetlandPlant
            {
                scinameauthor = "ADIANTUM CAPILLUS-VENERIS",
                commonname = "Common Maidenhair",
                family = "Pteridaceae (Adiantaceae)",
                topimgtopimg = "ADCA_1.jpg"
            });
            wetlandPlants.Add(new WetlandPlant
            {
                scinameauthor = "AGALINIS TENUIFOLIA",
                commonname = "Slenderleaf False Foxglove",
                family = "Scrophulariaceae (Orobanchaceae)",
                topimgtopimg = "AGTE3_1.jpg"
            });
            return wetlandPlants;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (wetlandPlantsList.SelectedItem != null)
            {
                var selectedItem = e.SelectedItem as WetlandPlant;
                var detailPage = new PortableApp.Views.WetlandPlantDetailPage(selectedItem);
                detailPage.BindingContext = selectedItem;
                wetlandPlantsList.SelectedItem = null;
                await Navigation.PushAsync(detailPage);
            }
        }
    }

    public class WetlandPlantsItemTemplate : ViewCell
    {
        public WetlandPlantsItemTemplate()
        {
            // Construct grid, the cell container
            Grid cell = new Grid
            {
                BackgroundColor = Color.FromHex("88000000"),
                Padding = new Thickness(20, 5, 20, 5),
                Margin = new Thickness(0, 0, 0, 10)
            };
            cell.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });
            cell.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.7, GridUnitType.Star) });
            cell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100) });

            // Add image
            var image = new Image { Aspect = Aspect.AspectFill, Margin = new Thickness(0, 0, 0, 20) };
            image.SetBinding(Image.SourceProperty, new Binding("ThumbnailPath"));
            cell.Children.Add(image, 0, 0);

            // Add text section
            StackLayout textSection = new StackLayout { Orientation = StackOrientation.Vertical, Spacing = 2 };

            var commonName = new Label
            {
                TextColor = Color.White,
                FontSize = 12,
                FontAttributes = FontAttributes.Bold | FontAttributes.Italic
            };
            commonName.SetBinding(Label.TextProperty, new Binding("scinamenoauthor"));
            textSection.Children.Add(commonName);

            var divider = new BoxView { HeightRequest = 1, WidthRequest = 500, BackgroundColor = Color.White };
            textSection.Children.Add(divider);

            var description = new Label
            {
                TextColor = Color.White,
                FontSize = 12
            };
            description.SetBinding(Label.TextProperty, new Binding("commonname"));
            textSection.Children.Add(description);

            var description2 = new Label
            {
                TextColor = Color.White,
                FontSize = 12
            };
            description2.SetBinding(Label.TextProperty, new Binding("family"));
            textSection.Children.Add(description2);

            cell.Children.Add(textSection, 1, 0);
            View = cell;
        }

    }

}
