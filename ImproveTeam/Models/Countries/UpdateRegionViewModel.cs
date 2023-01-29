using System.ComponentModel.DataAnnotations;

namespace ImproveTeam.Models.Countries
{
    public class UpdateRegionViewModel
    {
        public int RegionId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
