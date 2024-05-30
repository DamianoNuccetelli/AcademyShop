using AcademyShopAPI.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ManageProdottoBusiness
    {
        private readonly ManageProdottoData oPDL; //Oggetto Prodotto Data Layer

        public ManageProdottoBusiness(ManageProdottoData _oPDL)
        {
            oPDL = _oPDL;
        }

        public async Task<IEnumerable<Prodotto>> GetProdottosAsync()
        {
            return await oPDL.GetProdottosAsync();
        }
        public async Task<Prodotto> GetProdottoAsync(int id)
        {
            return await oPDL.GetProdottoAsync(id);
        }
        public async Task<Prodotto> AddProdottoAsync(Prodotto prodotto)
        {
            return await oPDL.AddProdottoAsync(prodotto);
        }
        public async Task<bool> UpdateProdottoAsync(Prodotto prodotto)
        {
            return await oPDL.UpdateProdottoAsync(prodotto);
        }   
        public async Task<bool> DeleteProdottoAsync(int id)
        {
            return await oPDL.DeleteProdottoAsync(id);
        }
        public async Task<bool> ProdottoExistsAsync(int id)
        {
            return await oPDL.ProdottoExistsAsync(id);
        }
        

    }
}
