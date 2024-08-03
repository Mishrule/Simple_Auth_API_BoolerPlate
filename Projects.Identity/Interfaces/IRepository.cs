using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Identity.Interfaces
{
    public interface IRepository<T, Tkey> 
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool useCache = false);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task InsertAsync(T entity, bool saveChanges = true);
       
        Task DeleteAsync(T entity, bool saveChanges = true);
        Task DeleteAllAsync(List<T> entities, bool saveChanges = true);
        Task UpdateAsync(T entity, bool saveChanges = true);
        Task<T?> FindByIdAsync(int id, bool useCache = false);

        Task<bool> SaveChangesAsync();
    }
}
