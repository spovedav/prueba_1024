using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.CORE.Interfaces.Repositorio
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Find(object parameters);
        void Add(T entity);
        T GetById(object parameters);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(object parameters);
    }
}
