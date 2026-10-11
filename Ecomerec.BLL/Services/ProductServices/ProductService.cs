using Ecomerec.BLL.DTOs;
using Ecomerec.BLL.Exceptions;
using Ecomerec.DAL;

namespace Ecomerec.BLL.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<ProductReadDTO>> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchWord = null,
            string? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            var products = await _productRepo.GetAllAsync();
            if (!products.Any())
                throw new BllException("Not Product Found");

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                products = products.Where(p => p.ProductName != null && p.ProductName.Contains(searchWord, StringComparison.OrdinalIgnoreCase) ||
                p.Description != null && p.Description.Contains(searchWord, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                products = products.Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));

            }
            if (minPrice.HasValue && minPrice.Value > 0)
            {
                products = products.Where(x => x.Price >= minPrice);
            }
            if (maxPrice.HasValue && maxPrice.Value > 0)
            {
                products = products.Where(x => x.Price <= maxPrice);
            }
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Clamp(pageSize, 1, 50);

            var pagedProducts =
            products.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

            return pagedProducts.Select(p => new ProductReadDTO
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                StockQuantity = p.StockQuantity
            }).ToList();


        }
        public async Task<ProductReadDTO> GetByIdAsync(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product is null)
            {
                throw new BllException("This Product is not found..!");
            }

            return new ProductReadDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Category = product.Category,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }

        public async Task<int> AddAsync(ProductWriteDTO dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                Description = dto.Description,
                Category = dto.Category,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            _productRepo.Add(product);
            await _productRepo.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> UpdateAsync(int id, ProductUpdateDTO dto)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product is null)
            {
                throw new BllException("This Product is not found..!");
            }

            product.ProductName = dto.ProductName;
            product.Description = dto.Description;
            product.Category = dto.Category;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;

            _productRepo.Update(product);
            await _productRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product is null)
            {
                throw new BllException("This Product is not found..!");
            }

            _productRepo.Delete(product);
            await _productRepo.SaveChangesAsync();
            return true;
        }
    }
}