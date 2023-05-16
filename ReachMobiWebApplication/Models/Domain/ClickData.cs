using System.ComponentModel.DataAnnotations.Schema;

namespace ReachMobiWebApplication.Models.Domain
{
    public class ClickData
    {
        public Guid Id { get; set; }
        public long trackId { get; set; }
        public DateTime timestamp { get; set; }
    }
}
