using System.ComponentModel.DataAnnotations;

namespace ImproveTeam.Models.Products
{
    public class AddProductViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
