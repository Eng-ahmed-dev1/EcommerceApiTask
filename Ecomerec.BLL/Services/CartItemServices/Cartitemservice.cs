using Ecomerec.BLL.DTOs;
using Ecomerec.BLL.Exceptions;
using Ecomerec.DAL;

namespace Ecomerec.BLL.Services.CartItemServices
{
    public class Cartitemservice : ICartitemservice
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;
        public Cartitemservice(ICartRepository cart, IProductRepository product)
        {
            _cartRepo = cart;
            _productRepo = product;
        }
        public async Task<int> AddAsync(CartItemWriteDTO dto)
        {
            if (dto.Quantity <= 0)
                throw new BllException("Invalid Quantity..!");

            var cart = await _cartRepo.GetByIdAsync(dto.CartId);
            if (cart is null)
                throw new BllException("This cart is not found ..!");

            var product = await _productRepo.GetByIdAsync(dto.ProductId);
            if (product is null)
                throw new BllException("This Product is not found ..!");

            if (dto.Quantity > product.StockQuantity)
                throw new BllException("The stock quantity is not available");

            var existProduct = cart.cartItems.FirstOrDefault(x => x.ProductId == dto.ProductId);
            CartItem item = null!;
            if (existProduct is not null)
            {
                if (existProduct.Quantity + dto.Quantity > product.StockQuantity)
                    throw new BllException("The stock quantity is not available");

                existProduct.Quantity += dto.Quantity;
            }
            else
            {
                item = new CartItem
                {
                    CartId = dto.CartId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    UnitPrice = product.Price
                };

                cart.cartItems.Add(item);
            }

            cart.TotalPrice += dto.Quantity * product.Price;
            _cartRepo.Update(cart);
            await _cartRepo.SaveChangesAsync();
            return existProduct?.Id ?? item.Id;
        }

        public async Task<bool> RemoveAsync(int cartid, int cartitemid)
        {
            var cart = await _cartRepo.GetByIdAsync(cartid);
            if (cart is null)
                throw new BllException("This cart is not found ..!");

            var existCart = cart.cartItems.FirstOrDefault(x => x.Id == cartitemid);
            if (existCart is null)
                throw new BllException("This CartItem is not found ..!");
            else
            {
                cart.cartItems.Remove(existCart);
                cart.TotalPrice -= existCart.UnitPrice * existCart.Quantity;
            }
            await _cartRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateQuantityAsync(CartItemUpdateDTO dto)
        {

            if (dto.Quantity <= 0)
                throw new BllException("Invalid Quantity..!");
            var cart = await _cartRepo.GetByIdAsync(dto.CartId);
            if (cart is null)
                throw new BllException("This cart is not found ..!");
            var product = await _productRepo.GetByIdAsync(dto.ProductId);
            if (product is null)
                throw new BllException("This Product is not found ..!");

            var cartItem = cart.cartItems.FirstOrDefault(x => x.ProductId == dto.ProductId);
            if (cartItem is null)
                throw new BllException("This Product is not found ..!");

            if (dto.Quantity > product.StockQuantity)
                throw new BllException("The stock quantity is not available");

            cartItem.Quantity = dto.Quantity;

            cart.TotalPrice = cart.cartItems.Sum(x => x.Quantity * x.UnitPrice);

            _cartRepo.Update(cart);
            await _cartRepo.SaveChangesAsync();
            return true;

        }
    }
}