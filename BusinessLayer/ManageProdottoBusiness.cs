using AcademyShopAPI.Models;
using DataLayer;
using Microsoft.IdentityModel.Tokens;
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
        private readonly ManageOrdineData oODL; //Oggetto Ordine Data Layer

        public ManageProdottoBusiness(ManageProdottoData _oPDL, ManageOrdineData _oODL)
        {
            oPDL = _oPDL;
            oODL = _oODL;
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
            try
            {
                if (string.IsNullOrEmpty(prodotto.Nome))
                {
                    throw new ArgumentException("Il nome del prodotto è obbligatorio.");
                }

                if (prodotto.Quantità < 0 || !IsInteger(prodotto.Quantità.ToString()))
                {
                    throw new ArgumentException("Errore nell'inserimento della quantità del prodotto");
                }

                return await oPDL.AddProdottoAsync(prodotto);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Errore: " + ex.Message);
            }
        }
        public static bool IsInteger(string value)
        {
            // Controllo se il valore è nullo o vuoto
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            // Controllo se il valore può essere convertito in un intero
            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        } //Metodo usato da AddProdottoAsync

        public async Task<bool> UpdateProdottoAsync(Prodotto prodotto)
        {
            return await oPDL.UpdateProdottoAsync(prodotto);
        }
        public async Task<bool> DeleteProdottoAsync(int id)
        {
            try
            {
                var prodottoToDelete = await oPDL.GetProdottoAsync(id);
                if (prodottoToDelete == null)
                {
                    return false;
                }

                // Verifica se il prodotto è associato a ordini esistenti
                var ordiniAssociati = await oPDL.GetProdottoAsync(id);
                if (ordiniAssociati.DettaglioOrdines.ToString().IsNullOrEmpty())
                {
                    return false;
                }

                return await oPDL.DeleteProdottoAsync(id);

            } catch (Exception ex)
            {
                throw new Exception("Errore: Il prodotto che hai cercato di eliminare è associato ad un ordine, eliminare prima l'ordine corrispondente prima di poter eliminare il prodotto dal database.");
            }
            
        }
        public async Task<bool> ProdottoExistsAsync(int id)
        {
            return await oPDL.ProdottoExistsAsync(id);
        }


    }
}
