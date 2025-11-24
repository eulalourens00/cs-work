using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherViewer.models;
using WeatherViewer.services;

namespace WeatherViewer.viewmodels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private readonly WeatherService _weatherService;
        private WeatherData _currentWeather;
        private bool _isLoading;
        private string _selectedCity = "Moscow";
        public bool HasWeatherData => CurrentWeather != null;
        public WeatherViewModel()
        {
            _weatherService = new WeatherService();
            LoadWeatherCommand = new Command(async () => await LoadWeatherAsync());
            PopularCities = _weatherService.GetPopularCities();

            Task.Run(async () => await LoadWeatherAsync());
        }

        public WeatherData CurrentWeather
        {
            get => _currentWeather;
            set
            {
                _currentWeather = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasWeatherData));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
                Task.Run(async () => await LoadWeatherAsync());
            }
        }

        public List<City> PopularCities { get; }
        public Command LoadWeatherCommand { get; }

        private async Task LoadWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(SelectedCity))
                return;

            IsLoading = true;

            var weather = await _weatherService.GetWeatherAsync(SelectedCity);

            if (weather != null)
            {
                CurrentWeather = weather;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Не удалось загрузить данные о погоде", "OK");
            }

            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
