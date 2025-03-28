using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockAppProject.Models;
using StockAppProject.Options;
using StockAppProject.ServiceContracts;


namespace StockAppProject.Controllers
{
    public class TradeController(
        IOptions<SocialMediaLinksOptions> socialMediaLinksOptions,
        IOptions<TradingOptions> tradingOptions,
        IFinnhubService finnhubService) : Controller
    {
        private readonly IOptions<SocialMediaLinksOptions> _socialMediaLinksOptions = socialMediaLinksOptions;
        private readonly IOptions<TradingOptions> _tradingOptions = tradingOptions;
        private readonly IFinnhubService _finnhubService = finnhubService;

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            _tradingOptions.Value.DefaultStockSymbol = _tradingOptions.Value.DefaultStockSymbol ?? "AAPL";
            
            Dictionary<string, object>? profileDictionary = await
            _finnhubService!.GetCompanyProfile(_tradingOptions.Value.DefaultStockSymbol);
            Dictionary<string, object>? quoteDictionary = await
            _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);

            ViewBag.facebook = _socialMediaLinksOptions.Value.Facebook;
            ViewBag.twitter = _socialMediaLinksOptions.Value.Twitter;
            ViewBag.instagram = _socialMediaLinksOptions.Value.Instragram;
            ViewBag.youtube = _socialMediaLinksOptions.Value.Youtube;

            StockTrade stockTrade = new()
            {
                StockName = profileDictionary?["name"].ToString(),
                StockSymbol = profileDictionary?["ticker"].ToString(),
                Price = Convert.ToDouble(quoteDictionary?["c"].ToString()),
                Quantity = Convert.ToInt32(quoteDictionary?["t"].ToString()),
            };
            return View(stockTrade);
        }
    }
}
