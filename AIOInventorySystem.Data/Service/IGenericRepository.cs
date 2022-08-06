using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIOInventorySystem.Data.Service
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        //IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Remove(T entity);
        void Edit(T entity);
        void Save();
        T GetById(int id);


    }
}
