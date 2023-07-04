using PaginationWEBAPI.DAL.DTO;
using PaginationWEBAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationWEBAPI.DAL.Repositories.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<ProductResponseDTO> GetProductsByPage(int page);
    }
}
