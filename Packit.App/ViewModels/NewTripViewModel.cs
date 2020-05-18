﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.ViewModels
{
    public class NewTripViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Trip> tripsDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly IBasicDataAccess<Backpack> backpacksDataAcess = new BasicDataAccessFactory<Backpack>().Create();
        private readonly IRelationDataAccess<Trip, Backpack> tripBackpackDataAccess = new RelationDataAccessFactory<Trip, Backpack>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private ICommand loadedCommand;
        private StorageFile localImage;
        private BitmapImage tripImage;

        public Trip Trip { get; set; } = new Trip();
        public BitmapImage TripImage
        {
            get => tripImage;
            set => Set(ref tripImage, value);
        }

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(async () => await LoadDataAsync()));
        public ICommand CandcelCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand ImageDeviceCommand { get; set; }
        public ICommand AddBackpackCommand { get; set; }
        public ICommand RemoveBackpackCommand { get; set; }

        public NewTripViewModel()
        {
            CandcelCommand = new RelayCommand(() => NavigationService.GoBack());

            ImageDeviceCommand = new RelayCommand(async () =>
            {
                localImage = await FileService.GetImageFromDevice();

                if (localImage == null)
                    return;

                TripImage = await FileService.StorageFileToBitmapImageAsync(localImage);
            });

            NextCommand = new RelayCommand(async () =>
            {
                if (localImage != null)
                {
                    var randomImageName = GenerateImageName();
                    Trip.ImageStringName = randomImageName;

                    if (!await tripsDataAccess.AddAsync(Trip) || !await imagesDataAccess.AddImageAsync(localImage, randomImageName))
                    {
                        await PopupService.ShowCouldNotSaveChangesAsync($"{Trip.Title} or {nameof(localImage)}");
                        return;
                    }

                    NavigationService.Navigate(typeof(SelectBackpacksPage));
                    return;
                }

                if (!await tripsDataAccess.AddAsync(Trip))
                {
                    await PopupService.ShowCouldNotSaveChangesAsync(Trip.Title);
                    return;
                }

                NavigationService.Navigate(typeof(SelectBackpacksPage), Trip);
            });
        }

        private async Task LoadDataAsync()
        {
            //await LoadBackpacksAsync();
            //LoadItemsInBackpacks();
        }
    }
}
