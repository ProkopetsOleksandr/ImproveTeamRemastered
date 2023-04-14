using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImproveTeam.Models.Advertiser
{
    public class UpdateAdvertiserViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public List<int> ProductIds { get; set; }
    }
}
