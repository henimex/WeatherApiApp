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

        public SearchCommand SearchCommand { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                SelectedCity = new City()
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
                            Value = 21
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }
        
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
                OnPropertyChanged("SelectedCity");
            }
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
            foreach (var city in Cities)
            {
                Cities.Add(city);
            }
        }

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
