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
                            Name = x["name"]["common"].ToString(),
                            Capital = x["capital"][0].ToString(),
                            Latitude = float.Parse(x["capitalInfo"]["latlng"][0].ToString()),
                            Longitude = float.Parse(x["capitalInfo"]["latlng"][1].ToString()),
                            CarsDriveOnSide = x["car"]["side"].ToString(),
                            StartOfWeek = x["startOfWeek"].ToString(),
                            NumberOfLanguages = x["languages"].Count(),
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
                return Countries.OrderBy(x => x.Population).Take(5).ToList();
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
                CountryModel country = Countries.FirstOrDefault(x => x.Equals(countryName));
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