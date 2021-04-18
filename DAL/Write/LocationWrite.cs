using System.Threading.Tasks;
using DAL.EntityModels;

namespace DAL.Write
{
    public class LocationWrite
    {

        public async Task CreateLocation(Location location)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                context.Locations.Add(location);
                await context.SaveChangesAsync();
            }
        }
        
        public async Task UpdateLocation(Location newLocation)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                Location existingEntity = context.Locations.Find(newLocation.Id);
                context.Entry(existingEntity).CurrentValues.SetValues(newLocation);
                await context.SaveChangesAsync();
            }
        }
        
    }
}