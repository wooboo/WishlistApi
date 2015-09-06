using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WishlistApi.Model.DataAccess
{
    public interface IRepository<T>
        where T:EntityBase
    {
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(string id);
        Task AddAsync(T data);
        Task UpdateAsync(string id, T data);
        Task DeleteAsync(string id);
    }
}