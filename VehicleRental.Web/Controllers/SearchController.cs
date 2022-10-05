using Microsoft.AspNetCore.Mvc;
using VehicleRental.DAL.Data;
using VehicleRental.Business;
using VehicleRental.DAL.Models;

namespace VehicleRental.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly masterContext _masterContext;
        private readonly IConfiguration _configuration;
        private readonly SearchLogic _searchLogic;

        public SearchController(masterContext context, IConfiguration configuration) {
            _masterContext = context;
            _configuration = configuration;
            _searchLogic = new SearchLogic(_masterContext,_configuration);
        }

        [Route("GetVehicles")]
        [HttpPost]
        public async Task<ActionResult<SearchResults>> GetVehicles([FromBody] BookingInputs bookingInputs)
        {
            try
            {
                return Ok(await _searchLogic.GetVehicles(bookingInputs));
            }

            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Route("GetVehiclesType")]
        [HttpGet]
        public async Task<ActionResult<List<string?>>> GetVehiclesType()
        {
            try
            {
                return Ok(await _searchLogic.GetVehiclestype());
            }

            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Route("GetCountries")]
        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            try
            {
                return Ok(await _searchLogic.GetCountries());
            }

            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
