using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReachMobiWebApplication.Models
{
    public class ItunesSearchApiResponse
    {
        public int resultCount { get; set; }
        public List<ItunesResult> results {get; set; }

        public Dictionary<string, List<ItunesResult>> GetItunesResultDict()
        {
            var itunesResultsDict = new Dictionary<string, List<ItunesResult>>();

            foreach (var item in results) 
            {
                if (item.kind.IsNullOrEmpty()) 
                {
                    item.kind = "Null";
                }

                if (!itunesResultsDict.ContainsKey(item.kind)) 
                {
                    itunesResultsDict[item.kind] = new List<ItunesResult>();
                }

                itunesResultsDict[item.kind].Add(item);
            }

            return itunesResultsDict;

        }
    }

    
}
