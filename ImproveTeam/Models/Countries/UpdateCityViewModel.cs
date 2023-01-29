using System.ComponentModel.DataAnnotations;

namespace ImproveTeam.Models.Countries
{
    public class UpdateCityViewModel
    {
        public int CityId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
