using Ecomerec.DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerec.DAL
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(int id);
        Task<Cart?> GetByUserIdAsync(int userId);
        void Add(Cart cart);
        void Update(Cart cart);
        Task<int> SaveChangesAsync();

    }
}