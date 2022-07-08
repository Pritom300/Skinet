
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
       
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;
        
        public ProductsController(IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            
           
        }

        [HttpGet]
        public async Task< ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string sort)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(sort);                                           //For Generic (For Just Include Statement)
            
            var product = await _productsRepo.ListAsync(spec);                                                  //*Generic    
                                                                                                                // return product.Select(product => new ProductToReturnDto{ 

                                                                                                                //     Id = product.Id,
                                                                                                                //     Name = product.Name,
            return Ok (_mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(product));                                                            //     Description = product.Description,
                                                                                                                //     PictureUrl = product.PictureUrl,                    //return Ok(product);
                                                                                                                //     Price = product.Price,
                                                                                                                //     ProductBrand = product.ProductBrand.Name,
                                                                                                                //     ProductType = product.ProductType.Name

                                                                                                                // }).ToList();                          
        }
 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);         //For Generic (For Just Include Statement and search id related product)
            var product =  await _productsRepo.GetEntityWithSpec(spec);   

                                                                                // return new ProductToReturnDto
            if (product == null) return NotFound(new ApiResponse(404));                                                                  // {
                                                                                //     Id = product.Id,
             return _mapper.Map<Product,ProductToReturnDto>(product);           //     Name = product.Name,
                                                                                //     Description = product.Description,
                                                                                //     PictureUrl = product.PictureUrl,          //return await _productsRepo.GetEntityWithSpec(spec);    *Generic
                                                                                //     Price = product.Price,
                                                                                //     ProductBrand = product.ProductBrand.Name,
                                                                                //     ProductType = product.ProductType.Name
                                                                                // };
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok (await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok (await _productTypeRepo.ListAllAsync());
        }

    }
}