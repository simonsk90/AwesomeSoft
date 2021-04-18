using System.Threading.Tasks;
using DAL.EntityModels;
using DAL.Read;
using DAL.Write;
using DTO;

namespace BusinessLogic
{
    public static class LocationService
    {

        public static async Task<LocationDto> GetLocation(int locationId)
        {
            Location locationEntity = await LocationRead.GetLocation(locationId);
            LocationDto result = Convert(locationEntity);
            return result;
        }
        
        public static async Task CreateLocation(LocationDto location)
        {
            Location locationEntity = Convert(location);
            await LocationWrite.CreateLocation(locationEntity);
        }
        
        public static async Task UpdateLocation(LocationDto location)
        {
            Location locationEntity = Convert(location);
            await LocationWrite.UpdateLocation(locationEntity);
        }
        
        public static LocationDto Convert(Location entity)
        {
            LocationDto result = new LocationDto()
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return result;
        }
        
        public static Location Convert(LocationDto locationDto)
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