using System.Data.Entity;
using System.Threading.Tasks;
using DAL.EntityModels;

namespace DAL.Read
{
    public static class LocationRead
    {
        public static async Task<Location> GetLocation(int locationId)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                return await context.Locations.FirstAsync(a => a.Id == locationId);
            }
        }
    }
}