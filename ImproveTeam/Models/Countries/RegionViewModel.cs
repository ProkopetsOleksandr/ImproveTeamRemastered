using System.Collections.Generic;

namespace ImproveTeam.Models.Countries
{
    public class RegionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CityViewModel> Cities { get; set; }
    }
}
