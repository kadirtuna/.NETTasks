using AutoMapper;
using PaginationWEBAPI.BLL.Infrastructure;
using PaginationWEBAPI.DAL.DTO;
using PaginationWEBAPI.DAL.Models;
using PaginationWEBAPI.DAL.Repositories;
using PaginationWEBAPI.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationWEBAPI.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository; //BU KISIM ÖMER BEY'E SORULACAK.
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ReadProductDTO> GetProductByIdAsync(int id)
        {
            if (id < 0)
                throw new Exception("Employee id can not be less than zero!");

            return _mapper.Map<ReadProductDTO>(await _productRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ReadProductDTO>> GetAllProductsAsync()
        {
            return _mapper.Map<IEnumerable<ReadProductDTO>>(await _productRepository.GetAllAsync());
        }

        public async Task<ProductResponseDTO> GetProductsByPage(int page)
        {
            return await _productRepository.GetProductsByPage(page);
        }

        public async Task<ProductDTO> InsertProductAsync(CreateProductDTO createProductDTO)
        {
            var addedProduct = await _productRepository.InsertAsync(_mapper.Map<Product>(createProductDTO));
            //await _repository.Save();

            return _mapper.Map<ProductDTO>(addedProduct);
        }

        public async Task UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            await _productRepository.Update(_mapper.Map<Product>(updateProductDTO));
        }

        public async Task<ProductDTO> DeleteProductAsync(int id)
        {
            var product = await _productRepository.DeleteAsync(id);
            await _productRepository.Save();

            return _mapper.Map<ProductDTO>(product);
        }

    }
}
