﻿using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface IBasicDataAccessHttp<T>
    {
        Task<bool> AddAsync(T entity);
        Task<bool> EditAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T[]> GetAllAsync();
    }
}
