using Ecomerec.BLL.DTOs;
using Ecomerec.BLL.Exceptions;
using Ecomerec.BLL.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerec.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productservice)
        {
            _productService = productservice;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ProductReadDTO>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchWord = null,
            string? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            try
            {
                return Ok(await _productService.GetAllAsync(pageNumber, pageSize, searchWord, category, minPrice, maxPrice));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ProductReadDTO>> GetByIdAsync(int id)
        {
            try
            {
                return Ok(await _productService.GetByIdAsync(id));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }
        [HttpPost("Add-Product")]
        public async Task<ActionResult<int>> AddAsync(ProductWriteDTO dto)
        {
            try
            {
                return Ok(await _productService.AddAsync(dto));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }
        [HttpPut("Update-Product")]
        public async Task<ActionResult<bool>> UpdateAsync(int id, ProductUpdateDTO dto)
        {
            try
            {
                return Ok(await _productService.UpdateAsync(id, dto));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }
        [HttpDelete("Remove-Prduct")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            try
            {
                return Ok(await _productService.DeleteAsync(id));
            }
            catch (BllException ex)
            {
                return BadRequest(ex?.Message);
            }
        }


    }
}
