using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {

        // // This is by using the non -repo pattern method
        // private readonly IProductRepository _repo;
        // public ProductsController(IProductRepository repo)
        // {
        //     _repo = repo;

        // }

        // @GENERIC-REPO-PATTERN, @TODO: Will be replaced with one repo soon.
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo,
         IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;

        }


        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            // Non- Repo pattern implementation 
            //return Ok(await _repo.GetProductsAsync());

            // @GENERIC-REPO-PATTERN,
            //var products = (await _productRepo.ListAllAsync());

            //@SPecification method implementation, not the new methd ListAsync
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductsWithFiltersWithCountSpecification(productParams);

            var totalItems = await _productRepo.CountAsync(countSpec);


            var products = (await _productRepo.ListAsync(spec));

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            // Without Auto Mapper
            // return products.Select(products => new ProductToReturnDto
            // {
            //     Id = products.Id,
            //     Name = products.Name,
            //     Description = products.Description,
            //     PictureUrl = products.PictureUrl,
            //     Price = products.Price,
            //     ProductBrand = products.ProductBrand.Name,
            //     ProductType = products.ProductType.Name

            // }).ToList();

            // With Auto Mapper
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageSize, productParams.PageSize, totalItems,data));



        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProducts(int id)
        {
            // Non- Repo pattern implementation 
            //return Ok(await _repo.GetProductByIdAsync(id));


            // @GENERIC-REPO-PATTERN
            //return Ok(await _productRepo.GetByIdAsync(id));

            //@SPecification method implementation, not the new methd GetEntityWithSpec

            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            //Adding 
            var products = await _productRepo.GetEntityWithSpec(spec);

            if (products == null) return NotFound((new ApiResponse(404)));

            // without Auto Mapper
            // return new ProductToReturnDto
            // {
            //     Id = products.Id,
            //     Name = products.Name,
            //     Description = products.Description,
            //     PictureUrl = products.PictureUrl,
            //     Price = products.Price,
            //     ProductBrand = products.ProductBrand.Name,
            //     ProductType = products.ProductType.Name

            // };

            // With Auto mapper
            return _mapper.Map<Product, ProductToReturnDto>(products);



        }

        [HttpGet("brands")]

        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            // Non- Repo pattern implementation 
            //return Ok(await _repo.GetProductBrandsAsync());
            // @GENERIC-REPO-PATTERN
            return Ok(await _productBrandRepo.ListAllAsync());
        }


        [HttpGet("types")]

        public async Task<ActionResult<ProductBrand>> GetProductTypes()
        {
            // Non- Repo pattern implementation 
            /// return Ok(await _repo.GetProductTypesAsync());
            // @GENERIC-REPO-PATTERN
            return Ok(await _productTypeRepo.ListAllAsync());
        }


    }

}