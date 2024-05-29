using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRepositoryAsync<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetDetailsAsync(int id);
        Task<T> AddAsync(T entity);
        Task<Boolean> UpdateAsync(T entity);
        Task<Boolean> DeleteAsync(int id);

        Task<T> AddUtenteAsync(T entity);

    }

    public interface IRepositorySyncronous<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
