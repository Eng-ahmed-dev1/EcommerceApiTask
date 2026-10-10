using Ecomerec.BLL.DTOs;

namespace Ecomerec.BLL.Services.CartItemServices
{
    public interface ICartitemservice
    {
        Task<int> AddAsync(CartItemWriteDTO dto);
        Task<bool> RemoveAsync(int cartId, int cartitemid);
        Task<bool> UpdateQuantityAsync(CartItemUpdateDTO dto);
    }
}