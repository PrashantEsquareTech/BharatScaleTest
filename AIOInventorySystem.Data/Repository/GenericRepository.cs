using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Service;

namespace AIOInventorySystem.Data.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    _entities.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private SwamiSamarthEntities _entities = new SwamiSamarthEntities();

        protected SwamiSamarthEntities Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }


        public virtual List<T> GetAll()
        {
            return _entities.Set<T>().ToList();
        }


        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Remove(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = System.Data.EntityState.Modified;
        }
        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public virtual T GetById(int id)
        {
            return _entities.Set<T>().Find(id);
        }



    }
}
