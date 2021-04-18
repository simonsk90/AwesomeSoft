using System;
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

        [Route("Location/{locationId}")]
        [HttpGet]
        public async Task<LocationDto> GetLocation(int locationId)
        {
            try
            {
                LocationDto result = await LocationService.GetLocation(locationId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [Route("Location")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateLocation([FromBody] LocationDto location)
        {
            HttpResponseMessage result;
            try
            {
                await LocationService.CreateLocation(location);
                result = Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }

            return result;
        }
        
        [Route("Location")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateLocation([FromBody] LocationDto location)
        {
            HttpResponseMessage result;
            try
            {
                await LocationService.UpdateLocation(location);
                result = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }

            return result;
        }
        
    }

}