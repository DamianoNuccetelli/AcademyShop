using AcademyShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class RepositoryProdotto : IRepositoryProdotto<Prodotto>
    {
        private readonly AcademyShopDBContext _context;

        public RepositoryProdotto(AcademyShopDBContext context)
        {
            _context = context;
        }

        public async Task<Prodotto> AddAsync(Prodotto prodotto)
        {
            await _context.Prodottos.AddAsync(prodotto);
            await _context.SaveChangesAsync();
            return prodotto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prodotto = await _context.Prodottos.FindAsync(id);
            if (prodotto == null)
            {
                return false;
            }

            _context.Prodottos.Remove(prodotto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Prodotto>> GetAllAsync()
        {
            return await _context.Prodottos.ToListAsync();
        }

        public async Task<Prodotto> GetByIdAsync(int id)
        {
            return await _context.Prodottos.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Prodotto prodotto)
        {
            var existingProdotto = await _context.Prodottos.FindAsync(prodotto.Id);
            if (existingProdotto == null)
            {
                return false;
            }

            _context.Entry(existingProdotto).CurrentValues.SetValues(prodotto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

