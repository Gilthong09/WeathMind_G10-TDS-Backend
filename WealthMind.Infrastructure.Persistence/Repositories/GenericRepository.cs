using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Infrastructure.Persistence.Contexts;

namespace WealthMind.Infrastructure.Persistence.Repository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext _dbContext;

        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return entity;
        }

        public virtual async Task UpdateAsync(Entity entity, string id)
        {
            var entry = await _dbContext.Set<Entity>().FindAsync(id);
            if (entry != null)
            {
                _dbContext.Entry(entry).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task UpdateWithNavigationsAsync(Entity entity, string id)
        {
            var entry = await _dbContext.Set<Entity>().FindAsync(id);
            if (entry != null)
            {
                _dbContext.Entry(entry).CurrentValues.SetValues(entity);

                foreach (var navigationProperty in _dbContext.Entry(entry).Navigations)
                {
                    if (navigationProperty.Metadata.IsCollection)
                    {
                        var currentCollection = (IEnumerable<object>)navigationProperty.CurrentValue!;
                        var newCollection = (IEnumerable<object>)navigationProperty.Metadata.PropertyInfo.GetValue(entity)!;

                        if (currentCollection != null)
                        {
                            foreach (var item in currentCollection.ToList())
                            {
                                _dbContext.Entry(item).State = EntityState.Deleted;
                            }
                        }

                        if (newCollection != null)
                        {
                            foreach (var item in newCollection)
                            {
                                _dbContext.Entry(item).State = EntityState.Added;
                            }
                        }
                    }
                    else
                    {
                        var newValue = navigationProperty.Metadata.PropertyInfo.GetValue(entity);
                        navigationProperty.CurrentValue = newValue;
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync(bool trackChanges = false)
        {
            return trackChanges
            ? await _dbContext.Set<Entity>().ToListAsync()
            : await _dbContext.Set<Entity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<List<Entity>> GetAllByUSerIdAsync(string userId, bool trackChanges = false)
        {
            IQueryable<Entity> query = trackChanges
            ? _dbContext.Set<Entity>()
            : _dbContext.Set<Entity>().AsNoTracking();

            var list = await query
                .Where(e => EF.Property<string>(e, "UserId") == userId)
                .ToListAsync();

            return list;
        }


        public virtual async Task<Entity> GetByIdAsync(string id, bool trackChanges = false)
        {
            return (await FindByCondition(e => EF.Property<string>(e, "Id") == id, trackChanges).FirstOrDefaultAsync())!;
        }

        public virtual async Task<Entity> GetByIdWithIncludeAsync(string id, List<string> properties, bool trackChanges = false)
        {
            var query = trackChanges ? _dbContext.Set<Entity>() : _dbContext.Set<Entity>().AsNoTracking();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.SingleOrDefaultAsync(e => EF.Property<string>(e, "Id") == id);
        }

        public virtual async Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties, bool trackChanges = false)
        {
            var query = FindAll(trackChanges);

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        private IQueryable<Entity> FindAll(bool trackChanges) =>
            !trackChanges ?
              _dbContext.Set<Entity>()
                .AsNoTracking() :
              _dbContext.Set<Entity>();

        private IQueryable<Entity> FindByCondition(Expression<Func<Entity, bool>> expression,
            bool trackChanges) =>
                !trackChanges ?
                  _dbContext.Set<Entity>()
                    .Where(expression)
                    .AsNoTracking() :
                  _dbContext.Set<Entity>()
                    .Where(expression);


    }
}
