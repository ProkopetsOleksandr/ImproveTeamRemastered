using System.Collections.Generic;

namespace ImproveTeam.Domain.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        List<City> Cities { get; set; }
    }
}
