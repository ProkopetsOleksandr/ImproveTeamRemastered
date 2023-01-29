using System.ComponentModel.DataAnnotations;

namespace ImproveTeam.Models.Countries
{
    public class AddRegionViewModel
    {
        public int CountryId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
