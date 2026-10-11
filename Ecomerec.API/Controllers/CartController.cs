using Ecomerec.BLL.DTOs;
using Ecomerec.BLL.Exceptions;
using Ecomerec.BLL.Services.CartItemServices;
using Ecomerec.BLL.Services.CartServices;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        //Task<bool> ClearAsync(int id);
        private readonly ICartService _cartservice;
        private readonly ICartitemservice _cartitemservice;
        public CartController(ICartService cartservice, ICartitemservice cartitemservice)
        {
            _cartservice = cartservice;
            _cartitemservice = cartitemservice;
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<CartReadDTO>> GetById([FromQuery] int id)
        {
            try
            {
                return Ok(await _cartservice.GetByIdAsync(id));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }
        [HttpGet("GetByUserId")]
        public async Task<ActionResult<CartReadDTO>> GetByUserId([FromRoute] int userId)
        {
            try
            {
                return Ok(await _cartservice.GetByUserIdAsync(userId));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }
        [HttpGet("Clear-Cart")]
        public async Task<ActionResult<bool>> ClearCart(int userId)
        {
            try
            {
                return Ok(await _cartservice.ClearAsync(userId));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }

        [HttpPost("Add-To-Cart")]
        public async Task<ActionResult> AddToCart(CartItemWriteDTO dto)
        {
            try
            { 
                return Ok(await _cartitemservice.AddAsync(dto));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }
        [HttpPost("Remove-From-Cart")]
        public async Task<ActionResult> RemoveFromCart(int cartid , int cartitemid)
        {
            try
            { 
                return Ok(await _cartitemservice.RemoveAsync(cartid, cartitemid));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }
        [HttpPost("Update-Quantity")]
        public async Task<ActionResult> UpdateProductQuantity(CartItemUpdateDTO dto)
        {
            try
            {
                return Ok(await _cartitemservice.UpdateQuantityAsync(dto));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }
    }
}
