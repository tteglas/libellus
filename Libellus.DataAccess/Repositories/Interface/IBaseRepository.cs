﻿using System.Collections.Generic;
using System.Linq;

namespace Libellus.DataAccess.Repositories.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Save();
    }
}
