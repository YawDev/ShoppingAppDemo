using System;
using System.Collections.Generic;

namespace ShoppingDemo.EFCore
{
    public interface IRepository<T>
    {
        int Commit();

        IEnumerable<T> GetAll();

        T GetById(Guid Id);

        void Add(T entity);

        void Delete(T Entity);
        void DeleteRange(IEnumerable<T> Entity);


    }
}
