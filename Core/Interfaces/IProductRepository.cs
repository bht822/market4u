using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        // Get the Product by Id
        Task<Product> GetProductByIdAsync(int id);

        // Read-only List of the Products all 
        Task<IReadOnlyList<Product>> GetProductsAsync();

        
    }
}