using System.Collections.Generic;

namespace ImproveTeam.Models.Countries
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<RegionViewModel> Regions { get; set; }
    }
}
