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
        T GetById(long id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(long id);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity); 
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(long id);
        Task RemoveRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

    }
}
