using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);  //get a particular row instance with id as primary Key as Id or id present as

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null); // get all the tables from the database of type T expression is to filter the data(u=>u.id==Id),
                                              //  includeProperties is used to join the table on the foreign key with other table

        T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null);  // getting the first result from the top of the database on null

         void Add(T entity);  // adding the entity to the database
          void Remove(string id); //removing the entity from the database with a particular id
        void Remove(T entity);   // removing the entity form the database
        void RemoveRange(IEnumerable<T> entities);  // removing multiple entities from the database

    }
}
