using Ecomerec.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecomerec.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Product Product)
        {
            _context.Products.Add(Product);
        }

        public void Delete(Product Product)
        {
            _context.Products.Remove(Product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(Product Product)
        {
            _context.Update(Product);
        }
    }
}