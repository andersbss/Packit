﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Packit.App.DataAccess;
using Packit.App.DataLinks;
using Packit.App.Factories;
using Packit.App.Helpers;
using Packit.App.Services;
using Packit.App.Views;
using Packit.Model;

namespace Packit.App.ViewModels
{
    public class TripsMainViewModel : ViewModel
    {
        private readonly IBasicDataAccess<Trip> tripsDataAccess = new BasicDataAccessFactory<Trip>().Create();
        private readonly ImagesDataAccess imagesDataAccess = new ImagesDataAccess();
        private ICommand loadedCommand;

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new NetworkErrorHandlingRelayCommand<TripsMainPage>(async () => await LoadDataAsync(), PopUpService));
        public ICommand TripDetailCommand { get; set; }
        public ICommand AddTripCommand { get; set; }

        public ObservableCollection<TripImageWeatherLink> Trips { get; } = new ObservableCollection<TripImageWeatherLink>();

        public TripsMainViewModel(IPopUpService popUpService)
            :base(popUpService)
        {
            TripDetailCommand = new RelayCommand<TripImageWeatherLink>(param =>
            {
                NavigationService.Navigate(typeof(DetailTripPage), param);
            });

            AddTripCommand = new RelayCommand(() => NavigationService.Navigate(typeof(NewTripPage)));
        }

        private async Task LoadDataAsync()
        {
            await LoadTrips();
            await LoadTripImagesAsync();
        }

        private async Task LoadTrips()
        {
            var trips = await tripsDataAccess.GetAllWithChildEntitiesAsync();

            foreach (Trip trip in trips)
                Trips.Add(new TripImageWeatherLink(trip));
        }
    
        private async Task LoadTripImagesAsync()
        {
            foreach (TripImageWeatherLink t in Trips)
                t.Image = await imagesDataAccess.GetImageAsync(t.Trip.ImageStringName, "ms-appx:///Assets/generictrip.jpg");
        }
    }
}
