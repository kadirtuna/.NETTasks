using Microsoft.EntityFrameworkCore;
using PaginationWEBAPI.DAL.DTO;
using PaginationWEBAPI.DAL.Models;
using PaginationWEBAPI.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationWEBAPI.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext) : base(dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<ProductResponseDTO> GetProductsByPage(int page)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_dataContext.Products.Count() / pageResults);

            var products = await _dataContext.Products
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new ProductResponseDTO()
            {
                Products = products,
                Pages = (int)pageCount,
                CurrentPage = page 
            };

            return response;
        }
    }
}
