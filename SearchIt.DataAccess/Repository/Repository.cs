using Microsoft.EntityFrameworkCore;
using SearchIt.DataAccess.Data;
using SearchIt.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet; //dbset contains all the entries of the table T
        public Repository(ApplicationDbContext db)
        {
            _db = db;  // getting the instance of ApplicationDbContext from the services to use here
            dbSet = db.Set<T>(); // getting the instance of the database T so can do operation on them without having the explicity mention db.Set<T> everytime
        }
        public void Add(T entity)
        {
            dbSet.Add(entity); // Inserting into the database
        }

        public T Get(int id)
        {
            return dbSet.Find(id); //finding and returning the record instance with the particular id as primary key
            
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter); // filter the data based on the lambda expression
            }
            if(includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)){
                    query = query.Include(property);   // join the table with the foreign key Specified Table Name using , as delimiter
                }
            }
            if(orderBy != null)
            {
                return orderBy(query).ToList();  
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)){
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();

        }

        public void Remove(string id)
        {
            T obj= dbSet.Find(id);
            this.Remove(obj);   
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);    
        }
    }
}
