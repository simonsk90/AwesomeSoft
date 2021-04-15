using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    // [RoutePrefix("api")]
    public class LocationController : ApiController
    {

        public LocationController()
        {
            
        }
        
        [Route("Location")]
        [HttpGet] 
        public IEnumerable<Location> GetAll()
        {
            return new Location[]
            {
                new Location { Name = "Loc1" },
                new Location { Name = "Loc2" },
                new Location { Name = "Loc3" }
            };
        }
        
        // [Route("Location")]
        // [HttpGet] 
        // public IEnumerable<Location> GetAll()
        // {
        //     return new Location[]
        //     {
        //         new Location { Name = "Ana" },
        //         new Location { Name = "Felipe" },
        //         new Location { Name = "Emillia" }
        //     };
        // }
        
        
    }

    public class Location
    {
        public string Name { get; set; }
    }
}