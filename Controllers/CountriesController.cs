using KAHA.TravelBot.NETCoreReactApp.Models;
using KAHA.TravelBot.NETCoreReactApp.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KAHA.TravelBot.NETCoreReactApp.Controllers
{
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ITravelBotService _travelBotService;
        public CountriesController(ITravelBotService travelBotService)
        {
            _travelBotService = travelBotService;
        }
        
        [HttpGet()]
        [Route("api/countries/topfive")]
        public async Task<IActionResult> GetTopFive()
        {
            try
            {
                var countries = await _travelBotService.GetTopFiveCountries();

                if (countries == null)
                {
                    return NotFound("No country records found");
                }
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        [Route("api/countries/{countryName}")]
        public async Task<IActionResult> GetSummary(string countryName)
        {
            try
            {
                var countrySummary = await _travelBotService.GetCountrySummary(countryName);


                if (countrySummary == null)
                {
                    return NotFound("No country summary generated");
                }
                return Ok(countrySummary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        [Route("api/countries/randomCountry")]
        public async Task<IActionResult> GetRandomCountry()
        {
            try
            {
                var country = await _travelBotService.RandomCountryInSouthernHemisphere();

                if (country == null)
                {
                    return NotFound("No country found");
                }
                return Ok(country);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
