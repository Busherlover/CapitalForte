using Finances.Models;
namespace Finances.Repository.IRepository
{
    public interface IScrapedDataYahooCryptoRepository : IRepository<ScrapedDataYahooCrypto>
    {
        void Update(ScrapedDataYahooCrypto obj);
    }
}