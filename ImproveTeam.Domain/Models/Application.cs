namespace ImproveTeam.Domain.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
    }
}
