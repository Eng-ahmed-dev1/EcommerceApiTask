using Ecomerec.BLL.DTOs;
namespace Ecomerec.BLL.Services.ProductServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDTO>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchWord = null,
            string? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null);
        Task<ProductReadDTO> GetByIdAsync(int id);
        Task<int> AddAsync(ProductWriteDTO dto);
        Task<bool> UpdateAsync(int id, ProductUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}