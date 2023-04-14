using System.ComponentModel.DataAnnotations;

namespace ImproveTeam.Models.Interests
{
    public class AddInterestViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
