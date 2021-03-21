using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        // Expression for the criteria, for filtering and pagination, e.g. product id = 1 , where criteria 
        Expression<Func<T, bool>> Criteria { get; }

        // Return the list of the criteria, return the object ( most generic "thing") in C#
        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>> OrderBy { get; }

        Expression<Func<T, object>> OrderByDescending { get; }

         int Take { get; }

         int Skip {get; }
         
         bool IsPagingEnables {get; }








    }
}