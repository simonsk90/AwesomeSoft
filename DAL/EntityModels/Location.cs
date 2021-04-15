using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.EntityModels
{
    public class Location
    {
        public Location()
        {
            
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


    }
}