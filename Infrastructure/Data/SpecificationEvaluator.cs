using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{

    // Could have use <T> but to be specific that the query will be dealing with DB used TEntity
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        // Return: IQueryable
        // Takes : IQueryable
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            // variable to store input query in
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);

            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);

            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);

            }

            // Paging is done best at the end

            if (spec.IsPagingEnables)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
                
            }


            // takes the .include statement(for the eadger/egar loading and adds to the query and returnt the "AGGREGATE) so we can query the database 
            // based on the query returned below
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;

        }

    }
}