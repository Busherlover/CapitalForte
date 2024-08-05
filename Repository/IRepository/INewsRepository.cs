using Finances.Models;

namespace Finances.Repository.IRepository
{
    public interface INewsRepository : IRepository<News>
    {
        void Update(News obj);
        
    }
}
