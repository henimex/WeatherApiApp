﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WheatherApiApp.Model;

namespace WheatherApiApp.ViewModel.Helpers
{
    public class AcuWeatherHelper
    {
        //locations/v1/cities/autocomplete?apikey=FbSDLkGxYlf60mXlZBTXMcnQGUkbTqHg&q=Eski%C5%9F
        //FbSDLkGxYlf60mXlZBTXMcnQGUkbTqHg
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLATE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}&language=tr-tr";
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}/?apikey={1}&language=tr-tr";
        public const string API_KEY = "FbSDLkGxYlf60mXlZBTXMcnQGUkbTqHg";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();
            
            string url = BASE_URL + string.Format(AUTOCOMPLATE_ENDPOINT, API_KEY, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }
            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();
            
            string url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
            }
            
            return currentConditions;
        }
    }
}
