using System.Data.Entity;
using System.Threading.Tasks;
using DAL.EntityModels;

namespace DAL.Write
{
    public static class LocationWrite
    {

        public static async Task CreateLocation(Location location)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                context.Locations.Add(location);
                await context.SaveChangesAsync();
            }
        }
        
        /// <summary>
        /// Update by overwriting everything
        /// </summary>
        /// <param name="newLocation"></param>
        /// <returns></returns>
        public static async Task UpdateLocation(Location newLocation)
        {
            using (AwesomeContext context = new AwesomeContext())
            {
                context.Locations.Attach(newLocation);
                context.Entry(newLocation).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
        
    }
}