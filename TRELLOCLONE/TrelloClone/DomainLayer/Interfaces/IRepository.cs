using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
	public interface IRepository<T> where T : class
	{
        Task<T> GetByIdAsync(long id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity); 
        Task RemoveAsync(long id);
        Task RemoveRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

    }
}
