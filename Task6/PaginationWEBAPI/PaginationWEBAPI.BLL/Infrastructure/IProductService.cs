using PaginationWEBAPI.DAL.DTO;
using PaginationWEBAPI.DAL.Models;
using PaginationWEBAPI.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationWEBAPI.BLL.Infrastructure
{
    public interface IProductService
    {
        public Task<ReadProductDTO> GetProductByIdAsync(int id);
        public Task<IEnumerable<ReadProductDTO>> GetAllProductsAsync();
        public Task<ProductResponseDTO> GetProductsByPage(int page);
        public Task<ProductDTO> InsertProductAsync(CreateProductDTO createProductDTO);
        public Task UpdateProduct(UpdateProductDTO updateProductDTO);
        public Task<ProductDTO> DeleteProductAsync(int id);
    }
}
