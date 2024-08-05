using Finances.Data;
using Finances.Models;
using Finances.Repository.IRepository;

namespace Finances.Repository
{
    public class ScrapedDataYahooCryptoRepository : Repository<ScrapedDataYahooCrypto>, IScrapedDataYahooCryptoRepository
    {
        private ApplicationDbContext _db;
        public ScrapedDataYahooCryptoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ScrapedDataYahooCrypto obj)
        {
            _db.ScrapedDataYahooCrypto.Update(obj);
        }
    }
}
