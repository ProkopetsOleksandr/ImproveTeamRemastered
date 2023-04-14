using System.ComponentModel.DataAnnotations;

namespace ImproveTeam.Models.Interests
{
    public class UpdateInterestViewModel
    {
        public int InterestId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
