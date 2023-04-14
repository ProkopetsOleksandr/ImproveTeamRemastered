using System.Collections.Generic;

namespace ImproveTeam.Domain.Models
{
    public class AdvertiserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<int> ProductIds { get; set; }
    }
}
