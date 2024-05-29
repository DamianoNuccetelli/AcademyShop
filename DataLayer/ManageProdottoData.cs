using AcademyShopAPI.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ManageProdottoData
    {
        private readonly AcademyShopDBContext _context;
        private readonly IRepositoryAsync<Prodotto> _prodottoRepository;

        public ManageProdottoData(AcademyShopDBContext context, IRepositoryAsync<Prodotto> prodottoRepository)
        {
            _context = context;
            _prodottoRepository = prodottoRepository;
        }
    }
}
