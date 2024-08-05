using Finances.Data;
using Finances.Models;
using Finances.Repository.IRepository;
using System.Linq.Expressions;

namespace Finances.Repository
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        private  ApplicationDbContext _db;
        public NewsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(News obj)
        {
            _db.News.Update(obj);
        }
    }
}
