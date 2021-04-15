using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EntityModels;

namespace DAL
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new AwesomeContext())
            {
                // Create and save a new Blog
                Console.Write("Enter a name for a new Location: ");
                var name = Console.ReadLine();

                var blog = new Location() { Name = name };

                try
                {
                    db.Locations.Add(blog);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                

                // Display all Blogs from the database
                var query = from b in db.Locations
                    orderby b.Name
                    select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
    
  
    
    // public class AwesomeContext : DbContext
    // {
    //     public AwesomeContext() : base("AwesomeSoft")
    //     {
    //         
    //     }
    //
    //     public AwesomeContext(string connectionStringName)
    //         : base(connectionStringName)
    //     {
    //         this.Database.Connection.ConnectionString = connectionStringName;
    //     }
    //     
    //     public DbSet<Location> Locations { get; set; }
    // }
    
}