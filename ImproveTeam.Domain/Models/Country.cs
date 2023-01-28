using System.Collections.Generic;

namespace ImproveTeam.Domain.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Region> Regions { get; set; }
    }
}
