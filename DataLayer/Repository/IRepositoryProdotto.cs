using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRepositoryProdotto<Prodotto> 
    {
        Task<IEnumerable<Prodotto>> GetAllAsync();
        Task<Prodotto> GetByIdAsync(int id);
        Task<Prodotto> AddAsync(Prodotto prodotto);
        Task<bool> UpdateAsync(Prodotto prodotto);
        Task<bool> DeleteAsync(int id);
    }
}
