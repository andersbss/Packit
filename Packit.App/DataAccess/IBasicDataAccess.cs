﻿using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface IBasicDataAccess<T>
    {
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T[]> GetAllAsync();
        Task<T> GetByIdAsync(T entity);
        Task<T[]> GetAllWithChildEntitiesAsync();
        Task<T> GetByIdWithChildEntitiesAsync(T entity);
    }
}
