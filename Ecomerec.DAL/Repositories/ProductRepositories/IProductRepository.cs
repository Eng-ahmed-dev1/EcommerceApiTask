namespace Ecomerec.DAL
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        void Add(Product Product);
        void Update(Product Product);
        void Delete(Product Product);
        Task<int> SaveChangesAsync();
    }
}