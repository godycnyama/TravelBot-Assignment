using KAHA.TravelBot.NETCoreReactApp.Models;

namespace KAHA.TravelBot.NETCoreReactApp.Services;
public interface ITravelBotService
{
    List<CountryModel> Countries { get; set; }

    Task<List<CountryModel>> GetAllCountries();
    Task<CountrySummaryModel> GetCountrySummary(string countryName);
    Task<List<CountryModel>> GetTopFiveCountries();
    Task<CountryModel> RandomCountryInSouthernHemisphere();
}