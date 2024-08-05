using Finances.Data;
using Finances.Models;
using Finances.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Finances.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository CategoryRepository { get; private set; }
        public INewsRepository NewsRepository { get; private set; }

        public IScrapedDataYahooCryptoRepository ScrapedDataYahooCryptoRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CategoryRepository = new CategoryRepository(_db);
            NewsRepository = new NewsRepository(_db);
            ScrapedDataYahooCryptoRepository = new ScrapedDataYahooCryptoRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();  
        }

        public async Task<int> SaveChangesAsync()
        {
            // Save changes asynchronously
            return await _db.SaveChangesAsync();
        }
    }
}
