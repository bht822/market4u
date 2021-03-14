using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    // Generic repo where <T > is evaluated at the runtime and the BaseENtity can be anything Product , ProductBrand or ProductType
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // add task which returns the type <T> which is passed at complie time
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec); 

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec );

    }
}