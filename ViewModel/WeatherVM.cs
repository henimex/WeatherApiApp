using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WheatherApiApp.Model;
using WheatherApiApp.ViewModel.Commands;
using WheatherApiApp.ViewModel.Helpers;

namespace WheatherApiApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }
        
        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                if (selectedCity!=null)
                {
                    OnPropertyChanged("SelectedCity");
                    GetCurrentConditions();
                }
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "Eskişehir"
                };
                CurrentConditions = new CurrentConditions()
                {
                    WeatherText = "Cloudy",
                    Temperature = new Temperature()
                    {
                        Metric = new Units()
                        {
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void MakeQuery()
        {
            var cities = await AcuWeatherHelper.GetCities(Query);
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            CurrentConditions = await AcuWeatherHelper.GetCurrentConditions(selectedCity.Key);
            Cities.Clear();

        }

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
