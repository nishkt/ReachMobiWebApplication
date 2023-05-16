using Newtonsoft.Json;
using ReachMobiWebApplication.Data;
using ReachMobiWebApplication.Models;

namespace ReachMobiWebApplication.Models
{
    public class ClickOccurences
    {
        public long trackId { get; set; }
        public int numberOfOccurences { get; set; }
        public ItunesResult itunesResult {get;set;}
    }
}
