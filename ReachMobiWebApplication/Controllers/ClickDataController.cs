using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReachMobiWebApplication.Data;
using ReachMobiWebApplication.Models;

namespace ReachMobiWebApplication.Controllers
{
    public class ClickDataController : Controller
    {
        private readonly ReachMobiDbContext myReachMobiDbContext;

        public ClickDataController(ReachMobiDbContext myReachMobiDbContext)
        {
            this.myReachMobiDbContext = myReachMobiDbContext;
        }

        public async Task<IActionResult> Index()
        {
            /*var clickdata = await myReachMobiDbContext.ClickData.ToListAsync();
            return View(clickdata);*/

            var count = new List<ClickOccurences>();

            try 
            {
                count = await myReachMobiDbContext.ClickData
                        .GroupBy(cd => cd.trackId)
                        .Select(g => new ClickOccurences { trackId = g.Key, numberOfOccurences = g.Count() })
                        .OrderByDescending(cd => cd.numberOfOccurences)
                        .ToListAsync();

                foreach (ClickOccurences item in count)
                {
                    ItunesSearchApiResponse itunesSearchResponse = new ItunesSearchApiResponse();
                    string collectionIdString = item.trackId.ToString();
                    using (var httpClient = new HttpClient())
                    {
                        string itunesUrl = $"https://itunes.apple.com//lookup?id={collectionIdString}";
                        using (var response = await httpClient.GetAsync(itunesUrl))
                        {
                            var apiResponse = await response.Content.ReadAsStringAsync();
                            itunesSearchResponse = JsonConvert.DeserializeObject<ItunesSearchApiResponse>(apiResponse);
                        }
                    }

                    item.itunesResult = itunesSearchResponse.results.FirstOrDefault();

                }
            }
            catch(Exception ex) 
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Program));
                logger.Error("Error occured in ClickDataController/Index method with the following exception: ", ex);
            }


            return View(count);



        }
    }
}
