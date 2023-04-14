using System.Collections.Generic;

namespace ImproveTeam.Domain.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Application> Applications { get; set; }
    }
}
