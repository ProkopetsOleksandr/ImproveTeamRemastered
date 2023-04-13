using System.ComponentModel.DataAnnotations;

namespace ImproveTeam.Models.Products
{
    public class UpdateProductViewModel
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
