using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic;
using DTO;

namespace API.Controllers
{
    [RoutePrefix("api")]
    public class LocationController : ApiController
    {
        private readonly LocationService _locationService = new LocationService();
        
        public LocationController()
        {
        }
        
        [Route("Location")]
        [HttpPost]
        public async Task CreateLocation([FromBody] LocationDto location)
        {
            await _locationService.CreateLocation(location);
        }
        
        [Route("Location")]
        [HttpPut]
        public async Task UpdateLocation([FromBody] LocationDto location)
        {
            await _locationService.UpdateLocation(location);
        }
        
    }

}