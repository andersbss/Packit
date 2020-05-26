﻿using System;
using Packit.App.DataLinks;
using Packit.App.Helpers;
using Packit.App.DataAccess.Http;
using Packit.App.ThirdPartyApiModels;
using Packit.App.ThirdPartyApiModels.Openweathermap;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Packit.Model;
using Packit.App.Services;
using Packit.App.Views;
using Packit.App.DataAccess;
using Packit.App.Factories;
using Packit.App.Wrappers;
using System.Linq;
using Packit.Extensions;
using Packit.Model.Models;
using System.Collections.Generic;
using System.Net.Http;
using Packit.Exceptions;

namespace Packit.App.ViewModels
{
    public class DetailTripViewModel : ViewModel
    {
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private readonly WeatherDataAccess weatherDataAccess = new WeatherDataAccess();
        private readonly IBasicDataAccess<Backpack> backpackDataAccess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IBasicDataAccess<Item> itemDataAccess = new BasicDataAccessFactory<Item>().Create();
        private readonly IBasicDataAccess<Trip> tripDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IBasicDataAccess<Check> checksDataAccess = new BasicDataAccessFactory<Check>().Create();
        private readonly IRelationDataAccess<Backpack, Item> backpackItemDataAccess = new RelationDataAccessFactory<Backpack, Item>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> tripBackpackDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private ICommand loadedCommand;
        private Trip tripClone;
        private bool isVisible;
        private bool weatherReportIsLoaded;

        public bool IsVisible
        {
            get => isVisible;
            set => Set(ref isVisible, value);
        }

        public bool WeatherReportIsLoaded
        {
            get => weatherReportIsLoaded;
            set => Set(ref weatherReportIsLoaded, value);
        }

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<TripsMainPage>(async () => await LoadDataAsync(), PopUpService));
        public ICommand EditTripCommand { get; set; }
        public ICommand DeleteTripCommand { get; set; }
        public ICommand CancelTripCommand { get; set; }
        public ICommand AddBackpacksCommand { get; set; }
        public ICommand RemoveBackpackCommand { get; set; }
        public ICommand DeleteBackpackCommand { get; set; }
        public ICommand ShareBackpackCommand { get; set; }
        public ICommand AddItemToBackpackCommand { get; set; }
        public ICommand RemoveItemFromBackpackCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand ItemCheckedCommand { get; set; }

        public TripImageWeatherLink TripImageWeatherLink { get; set; }
        public ObservableCollection<BackpackWithItemsWithImages> Backpacks { get; } = new ObservableCollection<BackpackWithItemsWithImages>();

        public DetailTripViewModel(IPopUpService popUpService)
            : base(popUpService)
        {
            DeleteTripCommand = new RelayCommand(async () =>
            {
                await PopUpService.ShowDeleteDialogAsync(DeleteTripAsync, TripImageWeatherLink.Trip.Title);
            });

            RemoveItemFromBackpackCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                await PopUpService.ShowRemoveDialogAsync(RemoveItemFromBackpack, param, param.ItemImageLink.Item.Title, param.BackpackWithItemsWithImages.Backpack.Title);
            });

            DeleteItemCommand = new RelayCommand<ItemImageBackpackWrapper>(async param =>
            {
                await PopUpService.ShowDeleteDialogAsync(DeleteItem, param, param.ItemImageLink.Item.Title);
            });

            RemoveBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                await PopUpService.ShowRemoveDialogAsync(RemoveBackpack, param, param.Backpack.Title, TripImageWeatherLink.Trip.Title);
            });

            DeleteBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                await PopUpService.ShowDeleteDialogAsync(DeleteBackpack, param, param.Backpack.Title);
            });

            AddBackpacksCommand = new RelayCommand<ItemBackpackWrapper>(param =>
            {
                NavigationService.Navigate(typeof(SelectBackpacksPage), new BackpackWithItemsWithImagesTripWrapper() { BackpackWithItemsWithImages = Backpacks, TripImageWeatherLink = TripImageWeatherLink });
            });

            AddItemToBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(param =>
            {
                NavigationService.Navigate(typeof(SelectItemsPage), new BackpackWithItemsTripImageWeatherWrapper() { Backpack = param, Trip = TripImageWeatherLink });
            });

            ItemCheckedCommand = new NetworkErrorHandlingRelayCommand<ItemBackpackBoolWrapper, TripsMainPage>(async param =>
            {
                await CheckItemAsync(param);
            }, PopUpService);

            ShareBackpackCommand = new RelayCommand<BackpackWithItemsWithImages>(async param =>
            {
                param.Backpack.IsShared = true;

                if (!await backpackDataAccess.UpdateAsync(param.Backpack))
                    param.Backpack.IsShared = false;
            });

            EditTripCommand = new NetworkErrorHandlingRelayCommand<DetailTripPage>(async () =>
            {
                if (!IsVisible)
                    CloneTrip();

                if (IsVisible)
                    await Update();

                IsVisible = !IsVisible;
            }, PopUpService);
        }

        private async Task LoadDataAsync()
        {
            LoadBackpacks();
            LoadItemsInBackpacks();
            await LoadWeatherReportAsync();
            await LoadItemImagesAsync();
        }

        private async Task Update()
        {
            await UpdateTripAsync();
            await UpdateWeatherAsync();
        }

        private async Task DeleteBackpack(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (await backpackDataAccess.DeleteAsync(backpackWithItemsWithImages.Backpack))
                Backpacks.Remove(backpackWithItemsWithImages);
            else
                await PopUpService.ShowCouldNotDeleteAsync(backpackWithItemsWithImages.Backpack.Title);
        }

        private async Task RemoveBackpack(BackpackWithItemsWithImages backpackWithItemsWithImages)
        {
            if (await tripBackpackDataAccess.DeleteEntityFromEntityAsync(TripImageWeatherLink.Trip.TripId, backpackWithItemsWithImages.Backpack.BackpackId))
            {
                Backpacks.Remove(backpackWithItemsWithImages);
                TripImageWeatherLink.Trip.Backpacks.RemoveAll(x => x.BackpackId == backpackWithItemsWithImages.Backpack.BackpackId);
            }
            else
                await PopUpService.ShowCouldNotDeleteAsync(backpackWithItemsWithImages.Backpack.Title);
        }

        private async Task DeleteItem(ItemImageBackpackWrapper itemImageBackpackWrapper)
        {
            if (await itemDataAccess.DeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item))
                itemImageBackpackWrapper.BackpackWithItemsWithImages.ItemImageLinks.Remove(itemImageBackpackWrapper.ItemImageLink);
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item.Title);
        }

        private async Task RemoveItemFromBackpack(ItemImageBackpackWrapper itemImageBackpackWrapper)
        {
            if (await backpackItemDataAccess.DeleteEntityFromEntityAsync(itemImageBackpackWrapper.BackpackWithItemsWithImages.Backpack.BackpackId, itemImageBackpackWrapper.ItemImageLink.Item.ItemId))
            {
                itemImageBackpackWrapper.BackpackWithItemsWithImages.ItemImageLinks.Remove(itemImageBackpackWrapper.ItemImageLink);

                if (itemImageBackpackWrapper.ItemImageLink.Item.Check != null)
                {
                    if (await checksDataAccess.DeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item.Check))
                        itemImageBackpackWrapper.ItemImageLink.Item.Check.IsChecked = false;
                }
            }
            else
                await PopUpService.ShowCouldNotDeleteAsync(itemImageBackpackWrapper.ItemImageLink.Item.Title);
        }

        private async Task DeleteTripAsync()
        {
            if (await tripDataAccess.DeleteAsync(TripImageWeatherLink.Trip))
                NavigationService.Navigate(typeof(TripsMainPage));
            else
                await PopUpService.ShowCouldNotDeleteAsync(TripImageWeatherLink.Trip.Title);
        }

        private async Task CheckItemAsync(ItemBackpackBoolWrapper param)
        {
            var itemcheker = new ItemChecker
                (
                    param.Item,
                    param.BackpackWithItemsWithImages.Backpack,
                    TripImageWeatherLink.Trip,
                    param.IsChecked,
                    PopUpService,
                    checksDataAccess
                );

            await itemcheker.HandleItemCheck();
        }

        private async Task UpdateTripAsync()
        {
            if (StringIsEqual(TripImageWeatherLink.Trip.Title, tripClone.Title) && StringIsEqual(TripImageWeatherLink.Trip.Destination, tripClone.Destination))
                return;

            TripImageWeatherLink.Trip.Backpacks.Clear();

            try
            {
                if (!await tripDataAccess.UpdateAsync(TripImageWeatherLink.Trip))
                    TripImageWeatherLink.Trip = tripClone;
            }
            catch (HttpRequestException)
            {
                TripImageWeatherLink.Trip = tripClone;
                throw;
            }
            catch (NetworkConnectionException)
            {
                TripImageWeatherLink.Trip = tripClone;
                throw;
            }
        }

        private async Task UpdateWeatherAsync()
        {
            if (StringIsEqual(TripImageWeatherLink.Trip.Destination, tripClone.Destination))
                return;
        
            await LoadWeatherReportAsync();
        }

        private void LoadBackpacks()
        {
            foreach(var bt in TripImageWeatherLink.Trip.Backpacks)
                Backpacks.Add(new BackpackWithItemsWithImages(bt.Backpack));
        }

        private void LoadItemsInBackpacks()
        {
            foreach (var bwi in Backpacks)
            {
                foreach (var itemBackpack in bwi.Backpack.Items)
                {
                    foreach (var check in itemBackpack.Item.Checks)
                    {
                        if (bwi.Backpack.BackpackId == check.BackpackId && itemBackpack.Item.ItemId == check.ItemId && TripImageWeatherLink.Trip.TripId == check.TripId)
                        {
                            itemBackpack.Item.Check = check;
                            itemBackpack.Item.Check.IsChecked = true;
                        }
                    }
                    bwi.ItemImageLinks.Add(new ItemImageLink() { Item = itemBackpack.Item });
                }
            }
        }

        private async Task LoadItemImagesAsync()
        {
            foreach (var bwi in Backpacks)
            {
                foreach (var itemImageLink in bwi.ItemImageLinks)
                {
                    itemImageLink.Image = await imagesDataAccess.GetImageAsync(itemImageLink.Item.ImageStringName, "ms-appx:///Assets/grey.jpg");
                }
            }
        }

        private async Task LoadWeatherReportAsync()
        {
            try
            {
                TripImageWeatherLink.WeatherReport = await weatherDataAccess.GetCurrentWeatherReportAsync(TripImageWeatherLink.Trip.Destination);
                TripImageWeatherLink.WeatherReport.Weathers[0].IconImage = await weatherDataAccess.GetCurrentWeatherIconAsync(TripImageWeatherLink.WeatherReport.Weathers[0].Icon);
                WeatherReportIsLoaded = true;
            }
            catch (HttpRequestException)
            {
                WeatherReportIsLoaded = false;
            }
        }

        public void Initialize(TripImageWeatherLink trip) => TripImageWeatherLink = trip;
        public void Initialize() => NavigationService.GoBack();
        private void CloneTrip() => tripClone = (Trip)TripImageWeatherLink.Trip.Clone();
    }
}
