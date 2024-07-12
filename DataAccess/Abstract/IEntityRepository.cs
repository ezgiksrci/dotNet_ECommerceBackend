using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    // Generic constraint (where ....)
    // class : Reference tip olmalı.
    // IEntity: IEntity veya IEntity implement eden class olmalı. 
    // new(): New'lenebilir olmalı.
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        // Expression'lar sayesinde method içerisine LINQ sorguları gönderebiliriz.
        // Func<input, result>
        T Get(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
    }
}
