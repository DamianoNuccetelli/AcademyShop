using AcademyShopAPI.Models;
using DataLayer.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ManageProdottoData
    {
        private readonly AcademyShopDBContext _context;
        private readonly IRepositoryProdotto<Prodotto> _prodottoRepository;

        public ManageProdottoData(AcademyShopDBContext context, IRepositoryProdotto<Prodotto> prodottoRepository)
        {
            _context = context;
            _prodottoRepository = prodottoRepository;
        }

        public async Task<IEnumerable<Prodotto>> GetProdottosAsync()
        {
            return await _prodottoRepository.GetAllAsync();
        }

        public async Task<Prodotto> GetProdottoAsync(int id)
        {
            return await _prodottoRepository.GetByIdAsync(id);
        }

        public async Task<Prodotto> AddProdottoAsync(Prodotto prodotto)
        {
            return await _prodottoRepository.AddAsync(prodotto);
        }

        public async Task<bool> UpdateProdottoAsync(Prodotto prodotto)
        {
            return await _prodottoRepository.UpdateAsync(prodotto);
        }

        public async Task<bool> DeleteProdottoAsync(int id)
        {
            return await _prodottoRepository.DeleteAsync(id);
        }

        public async Task<bool> ProdottoExistsAsync(int id)
        {
            return await _context.Prodottos.FindAsync(id) != null;
        }
    }
}
