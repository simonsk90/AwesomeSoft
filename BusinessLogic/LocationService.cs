using System.Threading.Tasks;
using DAL.EntityModels;
using DAL.Write;
using DTO;

namespace BusinessLogic
{
    public class LocationService
    {

        public async Task CreateLocation(LocationDto location)
        {
            Location locationEntity = Convert(location);
            LocationWrite locationWrite = new LocationWrite();
            await locationWrite.CreateLocation(locationEntity);
        }
        
        public async Task UpdateLocation(LocationDto location)
        {
            Location locationEntity = Convert(location);
            LocationWrite locationWrite = new LocationWrite();
            await locationWrite.UpdateLocation(locationEntity);
        }
        
        public LocationDto Convert(Location entity)
        {
            LocationDto result = new LocationDto()
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return result;
        }
        
        public Location Convert(LocationDto locationDto)
        {
            Location result = new Location()
            {
                Id = locationDto.Id,
                Name = locationDto.Name
            };
            return result;
        }
        
    }
}