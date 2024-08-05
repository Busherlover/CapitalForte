using Finances.Models;
using Finances.Repository.IRepository;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using NuGet.Protocol;
using Newtonsoft.Json;


namespace Finances.Areas.Customer.Controllers
{
    [Area("Customer")]
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<News> newsList = newsList = _unitOfWork.NewsRepository.GetAll() ;
            return View(newsList);
        }


        public IActionResult About_us()
        {
            return View();
        }

        public async Task<IActionResult> CryptocurrencyAsync()
        {
            IEnumerable<ScrapedDataYahooCrypto> cryptoList = _unitOfWork.ScrapedDataYahooCryptoRepository.GetAll().ToList();
            string url = "https://finance.yahoo.com/crypto/?count=100&offset=0";
            var uri = new Uri(url);
            HtmlWeb web = new HtmlWeb();
            HtmlNode nextButton = null;

            do
            {
                var doc = web.Load(url);
                var nodes = doc.DocumentNode.SelectNodes("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[6]/div/div/section/div/div[2]/div[1]/table/tbody/tr[position()>1]");
                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {


                        var cryptoRow = new ScrapedDataYahooCrypto
                        {
                            Symbol = node.SelectSingleNode("td[1]")?.InnerText.Trim(),
                            Name = node.SelectSingleNode("td[2]")?.InnerText.Trim(),
                            Price = node.SelectSingleNode("td[3]")?.InnerText.Trim(),
                            Change = node.SelectSingleNode("td[4]")?.InnerText.Trim(),
                            ChangeInProcentige = node.SelectSingleNode("td[5]")?.InnerText.Trim(),
                            MarketCap = node.SelectSingleNode("td[6]")?.InnerText.Trim(),
                            VolumeInCurrency = node.SelectSingleNode("td[7]")?.InnerText.Trim(),
                            VolumeOutCurrency24Hr = node.SelectSingleNode("td[8]")?.InnerText.Trim(),
                            TotalVolumeAllCurrencies24Hr = node.SelectSingleNode("td[9]")?.InnerText.Trim(),
                            CirculatingSupply = node.SelectSingleNode("td[10]")?.InnerText.Trim(),
                        };

                        var existingItem = cryptoList.FirstOrDefault(c => c.Name == cryptoRow.Name);


                        if (existingItem != null)
                        {
                            // Update existing item with new values
                            existingItem.Symbol = cryptoRow.Symbol;
                            existingItem.Price = cryptoRow.Price;
                            existingItem.Change = cryptoRow.Change;
                            existingItem.ChangeInProcentige = cryptoRow.ChangeInProcentige;
                            existingItem.MarketCap = cryptoRow.MarketCap;
                            existingItem.VolumeInCurrency = cryptoRow.VolumeInCurrency;
                            existingItem.VolumeOutCurrency24Hr = cryptoRow.VolumeOutCurrency24Hr;
                            existingItem.TotalVolumeAllCurrencies24Hr = cryptoRow.TotalVolumeAllCurrencies24Hr;
                            existingItem.CirculatingSupply = cryptoRow.CirculatingSupply;

                            _unitOfWork.ScrapedDataYahooCryptoRepository.Update(existingItem);
                        }
                        else
                        {
                            _unitOfWork.ScrapedDataYahooCryptoRepository.Add(cryptoRow);
                        }

                    };
                    _unitOfWork.Save();
                }




                nextButton = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[6]/div/div/section/div/div[2]/div[2]/button[3]");
                if (nextButton != null)
                {
                    var hrefValue = nextButton.GetAttributeValue("href", null);
                    if (!string.IsNullOrEmpty(hrefValue))
                    {
                        url = uri.Scheme + "://" + uri.Authority + hrefValue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            } while (nextButton != null && !nextButton.GetAttributeValue("class", "").Contains("disabled"));
           

            var data = await _unitOfWork.ScrapedDataYahooCryptoRepository.GetAllAsync();
            return View(data);
        }

        [HttpGet]
        public IActionResult GetCryptoData()
        {
            var data = _unitOfWork.ScrapedDataYahooCryptoRepository.GetAll().ToList();
            return Json(data);
        }

        public IActionResult InvestingStrategies()
        {
            return View();
        }
        public IActionResult InvestingStrategies2()
        {
            return View();
        }
        public IActionResult InvestingStrategies3()
        {
            return View();
        }

        public IActionResult Stocks()
        {
            return View();
        }

        public IActionResult Indices()
        {
            return View();
        }

        public IActionResult Gold_Silver()
        {
            return View();
        }

        public IActionResult ETF()
        {
            return View();
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
