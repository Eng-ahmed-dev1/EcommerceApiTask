using Ecomerec.BLL.DTOs;
using Ecomerec.BLL.Exceptions;
using Ecomerec.DAL;

namespace Ecomerec.BLL.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _context;

        public CartService(ICartRepository repository)
        {
            _context = repository;
        }

        public async Task<CartReadDTO> GetByIdAsync(int id)
        {
            var cart = await _context.GetByIdAsync(id);
            if (cart is null)
            {
                throw new BllException("This Cart Is Not Found ..!");
            }

            return new CartReadDTO
            {
                Id = cart.Id,
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice,
                Email = cart.User?.Email ?? string.Empty,
                UserName = cart.User?.UserName ?? string.Empty,
                cartItems = cart.cartItems.Select(x => new CartItemReadDTO
                {
                    Id = x.Id,
                    ProductName = x.Product?.ProductName ?? string.Empty,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };
        }

        public async Task<CartReadDTO> GetByUserIdAsync(int userId)
        {
            var cart = await _context.GetByUserIdAsync(userId);
            if (cart is null)
            {
                throw new BllException("Cart for this user was not found ..!");
            }

            return new CartReadDTO
            {
                Id = cart.Id,
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice,
                Email = cart.User?.Email ?? string.Empty,
                UserName = cart.User?.UserName ?? string.Empty,
                cartItems = cart.cartItems.Select(x => new CartItemReadDTO
                {
                    Id = x.Id,
                    ProductName = x.Product?.ProductName ?? string.Empty,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };
        }

        public async Task<bool> ClearAsync(int id)
        {
            var cart = await _context.GetByIdAsync(id) ?? await _context.GetByUserIdAsync(id);
            if (cart is null)
            {
                throw new BllException("This Cart Is Not Found , Invalid Cart Id");
            }

            _context.ClearCart(cart);
            _context.Update(cart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}