﻿namespace WealthMind.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<List<Entity>> GetAllAsync(bool trackChanges = false);
        Task<List<Entity>> GetAllByUSerIdAsync(string userId, bool trackChanges = false);
        Task<Entity> GetByIdAsync(string id, bool trackChanges = false);
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, string id);
        Task UpdateWithNavigationsAsync(Entity entity, string id);
        Task DeleteAsync(Entity entity);
        Task<Entity> GetByIdWithIncludeAsync(string id, List<string> properties, bool trackChanges = false);
        Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties, bool trackChanges = false);

    }
}
