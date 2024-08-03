using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Projects.Identity.Interfaces;
using Projects.Identity.DbContext;

namespace Projects.Identity.Repositories.Base
{
    public class Repository<T, Tkey> : IRepository<T, Tkey> where T : class

    {
        protected readonly ProjectIdentityDbContext Context;
        
        private readonly ILogger<Repository<T, Tkey>> _logger;
        private DbSet<T> ?_dbSet;
        protected Repository(ProjectIdentityDbContext context,  ILogger<Repository<T, Tkey>> logger)
        {
            Context = context;
            
            _logger = logger;
        }

        protected virtual DbSet<T> Entities
            => _dbSet ??= Context.Set<T>();


        protected virtual IQueryable<T> GetBaseQuery()
            => Entities;

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            try
            {
                predicate ??= x => true;
                var results = await GetBaseQuery().Where(predicate).ToListAsync();
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}", e.Message);
                throw;
            }
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool useCache = false)
        {
            try
            {
                if (predicate == null) return null;


                return await GetBaseQuery().FirstOrDefaultAsync(predicate);
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}", e.Message);
                throw;
            }
        }

        public async Task InsertAsync(T entity, bool saveChanges = true)
        {
            try
            {
                await Entities.AddAsync(entity);
                if (saveChanges)
                    await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}", e.Message);
                throw;
            }
        }

        public async Task UpdateAsync(T entity, bool saveChanges = true)
        {
            try
            {

                await Task.Run(() => Entities.Update(entity));
                if (saveChanges)
                    await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}", e.Message);
                throw;
            }
        }

        public async Task DeleteAsync(T entity, bool saveChanges = true)
        {
            try
            {

                await Task.Run(() => Entities.Remove(entity));
                if (saveChanges) await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}", e.Message);
                throw;
            }
        }

        public async Task DeleteAllAsync(List<T> entities, bool saveChanges = true)
        {
            try
            {

                await Task.Run(() =>
                {
                    foreach (var entity in entities)
                    {
                        Entities.Remove(entity);
                    }
                });

                if (saveChanges) await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}", e.Message);
                throw;
            }
        }



        public async Task<T?> FindByIdAsync(int id, bool useCache = false)
        {
            try
            {
                //if (id == 0) return null;
                var keyProperty = Context.Model
                                    .FindEntityType(typeof(T))?
                                    .FindPrimaryKey()?
                                    .Properties[0];



                return await GetBaseQuery().FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty!.Name) == id);
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}", e.Message);
                throw;
            }
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }



    }
}
