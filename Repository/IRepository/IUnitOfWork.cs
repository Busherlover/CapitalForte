namespace Finances.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        INewsRepository NewsRepository { get; }
        IScrapedDataYahooCryptoRepository ScrapedDataYahooCryptoRepository { get; }
        Task<int> SaveChangesAsync();

        void Save();

    }
}
