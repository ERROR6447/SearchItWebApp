﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.DataAccess.Repository.IRepositoryAsync
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null);

         Task AddAsync(T entity);
         Task RemoveAsync(string id);
         Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);

    }
}
