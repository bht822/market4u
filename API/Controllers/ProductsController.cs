using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
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

        // @GENERIC-REPO-PATTERN
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
            // @GENERIC-REPO-PATTERN
            return Ok(await _productRepo.ListAllAsync());


        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            // Non- Repo pattern implementation 
            //return Ok(await _repo.GetProductByIdAsync(id));
            // @GENERIC-REPO-PATTERN
            return Ok(await _productRepo.GetByIdAsync(id));


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