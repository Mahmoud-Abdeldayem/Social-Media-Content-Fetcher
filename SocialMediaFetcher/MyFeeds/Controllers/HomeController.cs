using Microsoft.AspNetCore.Mvc;
using MyFeeds.Models;
using MyFeeds.Services;
using System.Diagnostics;
using System.Net.Http;

namespace MyFeeds.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly FacebookFetcher _facebookFetcher;
        private readonly RedditFetcher _redditFetcher;
        private readonly FeedsManager _feedsManager;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient , FacebookFetcher facebookFetcher , RedditFetcher redditFetcher)
        {
            _logger = logger;
            _httpClient = httpClient;
            _facebookFetcher = facebookFetcher;
            _redditFetcher = redditFetcher;
            _feedsManager = new FeedsManager(_facebookFetcher , _redditFetcher);
        }

        public IActionResult Index()
        {
            _feedsManager.FetchFromAll();
            var feeds = FeedsStore.GetAllFeeds();
            return View(feeds);
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
