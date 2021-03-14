using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public class ProductsController : ControllerBase
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

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo)
        {
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;

        }


        [HttpGet]

        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            // Non- Repo pattern implementation 
            //return Ok(await _repo.GetProductsAsync());

            // @GENERIC-REPO-PATTERN,
            //var products = (await _productRepo.ListAllAsync());

            //@SPecification method implementation, not the new methd ListAsync
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = (await _productRepo.ListAsync(spec));


            return Ok(products);


        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            // Non- Repo pattern implementation 
            //return Ok(await _repo.GetProductByIdAsync(id));


            // @GENERIC-REPO-PATTERN
            //return Ok(await _productRepo.GetByIdAsync(id));

            //@SPecification method implementation, not the new methd GetEntityWithSpec

            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            return Ok(await _productRepo.GetEntityWithSpec(spec));


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