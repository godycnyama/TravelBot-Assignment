using System;
using System.Text.Json.Nodes;
using KAHA.TravelBot.NETCoreReactApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KAHA.TravelBot.NETCoreReactApp.Services
{
    public class TravelBotService : ITravelBotService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _restCountriesHttpClient;
        private readonly HttpClient _sunriseSunsetHttpClient;
        public List<CountryModel> Countries { get; set; }
        public TravelBotService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _restCountriesHttpClient = _httpClientFactory.CreateClient("RestCountries");
            _sunriseSunsetHttpClient = _httpClientFactory.CreateClient("SunriseSunset");
        }

        public async Task<List<CountryModel>> GetAllCountries()
        {
            var response = await _restCountriesHttpClient.GetStringAsync("all");
            var parsed_Response = JArray.Parse(response);
            var countries = new List<CountryModel>();
            if (parsed_Response.Count > 0)
            {
                foreach (var x in parsed_Response)
                {
                    try
                    {
                        var country = new CountryModel
                        {
                            Name = x["name"]["common"]?.ToString(),
                            Capital = x["capital"]?.First?.ToString(),
                            Population = x["population"] != null ? int.Parse(x["population"]?.ToString()) : 0,
                            Latitude = x["capitalInfo"]?["latlng"]?.First != null ? float.Parse(x["capitalInfo"]["latlng"].First.ToString()) : 0,
                            Longitude = x["capitalInfo"]?["latlng"]?.Last != null ? float.Parse(x["capitalInfo"]["latlng"].Last.ToString()) : 0,
                            CarsDriveOnSide = x["car"]?["side"]?.ToString(),
                            StartOfWeek = x["startOfWeek"]?.ToString(),
                            NumberOfLanguages = x["languages"]?.Count() ?? 0
                        };
                        countries.Add(country);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return countries;
        }

        // Top 5 Countries by population size
        public async Task<List<CountryModel>> GetTopFiveCountries()
        {
            try
            {
                Countries = await GetAllCountries();
                return Countries.OrderByDescending(x => x.Population).Take(5).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get country summary
        public async Task<CountrySummaryModel> GetCountrySummary(string countryName)
        {
            try
            {
                Countries = await GetAllCountries();
                CountryModel country = Countries.Where(x => x.Name.Equals(countryName)).FirstOrDefault();
                SunriseSunsetResponse sunriseSunsetTimes = await GetSunriseSunsetTimes(country.Latitude, country.Longitude);
                return new CountrySummaryModel
                {
                    Name = country.Name,
                    Longitude = country.Longitude,
                    Latitude = country.Latitude,
                    Capital = country.Capital,
                    CarsDriveOnSide = country.CarsDriveOnSide,
                    NumberOfLanguages = country.NumberOfLanguages,
                    Population = country.Population,
                    StartOfWeek = country.StartOfWeek,
                    Sunrise = sunriseSunsetTimes.Results.Sunrise,
                    Sunset = sunriseSunsetTimes.Results.Sunset
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CountryModel> RandomCountryInSouthernHemisphere()
        {
            Countries = await GetAllCountries();
            var countriesInSouthernHemisphere = Countries.Where(x => x.Latitude <= 0);
            var random = new Random();
            var randomIndex = random.Next(0, countriesInSouthernHemisphere.Count());
            return countriesInSouthernHemisphere.ElementAt(randomIndex);
        }

        private async Task<SunriseSunsetResponse> GetSunriseSunsetTimes(float lat, float lng)
        {
            try
            {

                var tomorrowDate = DateTime.Today.AddDays(1);
                var tomorrowDateString = tomorrowDate.ToString("yyyy-MM-dd");

                // get sunrise and sunset times for tomorrow from https://sunrise-sunset.org/api
                var response = await _sunriseSunsetHttpClient.GetStringAsync($"json?lat={lat}&lng={lng}&date={tomorrowDateString}");
                return JsonConvert.DeserializeObject<SunriseSunsetResponse>(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}