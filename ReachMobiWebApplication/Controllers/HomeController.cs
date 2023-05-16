using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReachMobiWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using ReachMobiWebApplication.Models.Domain;
using ReachMobiWebApplication.Data;
using static System.Net.WebRequestMethods;
using Microsoft.IdentityModel.Tokens;
using log4net.Config;

namespace ReachMobiWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReachMobiDbContext myReachMobiDbContext;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ReachMobiDbContext myReachMobiDbContext)
        {
            _logger = logger;
            this.myReachMobiDbContext = myReachMobiDbContext;
        }

        public async Task<IActionResult> Index(String search)
        {
            ItunesSearchApiResponse itunesResults = new ItunesSearchApiResponse();

            try
            {
                //replace space in search string with +
                string urlSearch = new string(search);
                if (!search.IsNullOrEmpty())
                {
                    urlSearch = search.Replace(" ", "+");
                }

                using (var httpClient = new HttpClient())
                {
                    string itunesUrl = $"https://itunes.apple.com/search?term={urlSearch}&limit=500";
                    using (var response = await httpClient.GetAsync(itunesUrl))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        itunesResults = JsonConvert.DeserializeObject<ItunesSearchApiResponse>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Program));
                logger.Error("Error occured in HomeController/Index method with the following exception: ", ex);
            }

            //remove duplicates that have the same trackName
            itunesResults.results = itunesResults.results.GroupBy(x => x.trackName).Select(d => d.First()).ToList();

            var resultDict = itunesResults.GetItunesResultDict();

            return View(resultDict);
        }

        public async Task<IActionResult> AddClickItunesData(long trackId, string redirectUrl)
        {
            var clickdata = new ClickData()
            {
                Id = Guid.NewGuid(),
                trackId = trackId,
                timestamp = DateTime.Now,

            };

            await myReachMobiDbContext.ClickData.AddAsync(clickdata);
            await myReachMobiDbContext.SaveChangesAsync();


            return Redirect(redirectUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
