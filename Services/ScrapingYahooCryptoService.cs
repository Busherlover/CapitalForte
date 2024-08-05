using Finances.Models;
using Finances.Repository.IRepository;
using HtmlAgilityPack;

namespace Finances.Services
{
    public class ScrapingYahooCryptoService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public ScrapingYahooCryptoService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
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
                       
                            var existingItem = cryptoList.FirstOrDefault(c => c.Symbol == cryptoRow.Symbol);

                            if (existingItem != null)
                            {
                                                           
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

                            _unitOfWork.Save();
                        }
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
                
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
