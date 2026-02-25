using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tracker.Repositories;
using tracker.Services;
using CommunityToolkit.Mvvm.ComponentModel;
namespace tracker.ViewModels
{
    public partial class MainViewModel : ViewModel
    {
        private readonly ILocationRepository locationRepository;
        private readonly ILocationTrackingService locationTrackingService;

        [ObservableObject]
        private List<Models.Point> points;

        public MainViewModel(ILocationTrackingService locationTrackingService, ILocationRepository locationRepository)
        {
            this.locationTrackingService = locationTrackingService;
            this.locationRepository = locationRepository;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                locationTrackingService.StartTracking();
                await LoadDataAsync();
            });
        }

        private async Task LoadDataAsync()
        {
            var locations = await locationRepository.GetAllAsync();
            var pointList = new List<Models.Point>();

            foreach (var location in locations)
            {
                if (!pointList.Any())
                {
                    pointList.Add(new Models.Point() { Location = location });
                }
                pointList.Add(new Models.Point() { Location = location });
                continue;
            }
            var pointFound = false;

            foreach (var point in pointList)
            {
                var distance = Location.CalculateDistance(
                    new Location(point.Location.Latitude, point.Location.Longitude),
                    new Location(location.Latitude, location.Longitude),
                    DistanceUnits.Kilometers
                    );

                if (distance < 0.2)
                {
                    pointFound = true;
                    point.Count++;
                    break;
                }
            }
            if (!pointFound)
            {
                pointList.Add(new Models.Point() { Location = location });
            }

            if (pointList == null || !pointList.Any()) return;

            var pointMax = pointList.Select(x => x.Count).Max();
            var pointMin = pointList.Select(x => x.Count).Min();
            var diff = (float)pointMax - pointMin;

            foreach (var point in pointList)
            {
                var heat = (2f / 3f) - ((float)point.Count / diff);
                point.Heat = Color.FromHsla(heat, 1, 0.5);
            }
        }

        Points = pointList; 
       
    }  
}
