using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
	public interface IService<T> where T : class
	{
        T GetById(long id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(long id);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);
    }
}
