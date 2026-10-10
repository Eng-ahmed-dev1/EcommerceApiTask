using Ecomerec.BLL.DTOs;

namespace Ecomerec.BLL.Services.CartServices
{
    public interface ICartService
    {
        Task<CartReadDTO> GetByIdAsync(int id);
        Task<CartReadDTO> GetByUserIdAsync(int userId);
        Task<bool> ClearAsync(int id);
    }
}