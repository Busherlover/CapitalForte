using System.ComponentModel.DataAnnotations;

namespace Finances.Models
{
    public class ScrapedDataYahooCrypto
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Symbol {  get; set; }
        public string? Name { get; set; }
        public string? Price { get; set; }
        public string? Change { get; set; }
        public string? ChangeInProcentige { get; set; }
        public string? MarketCap { get; set; }
        public string? VolumeInCurrency { get; set; }
        public string? VolumeOutCurrency24Hr { get; set; }
        public string? TotalVolumeAllCurrencies24Hr { get; set; }
        public string? CirculatingSupply { get; set; }
    }
}
