using AcademyShopAPI.Models;
using DtoLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRepositoryOrdine
    {
        //Renato
        Task<List<OrdineDettaglioDTOperGET>> GetOrdiniByUserId(int userId);
        //Gabriele
        Task<OrdineDettaglioDTOperGET> GetOrdineDettaglioAsync(int userId, int dettaglioOrdineId);
        Task<string?> GetUserPassword(int userId);
        Task<bool> VerificaUserAsync(int userId, string password);
        //Damiano
        Task<Ordine?> RecuperaOrdineAsync(int idOrdineEsistente);
        Task<DettaglioOrdine?> RecuperaDettaglioOrdineAsync(int idDettaglioOrdine);
        Task<Prodotto?> RecuperaProdottoAsync(int idProdotto);
        Task<int?> RecuperaIdOrdineAsync(int idUtente, int idDettaglioOrdine);
        Task<int?> RecuperaStatoOrdineAsync(int idOrdineEsistente);
        Task<int?> RecuperaIdProdottoAsync(int idOrdineEsistente);
        Task<int?> RecuperaQuantitaProdottoAsync(int idProdotto);
        Task<bool> ModificaOrdineTransazioneAsync(Ordine ordine, DettaglioOrdine dettaglioOrdine, Prodotto prodotto, int quantita);
        Task<Ordine?> RecuperaOrdineModificatoAsync(int idOrdine);
        //Adriano
        Task<int> addOrdine(int idUtente, Prodotto prodotto, int quantità);
        Task<Prodotto> getProdottoAsync(int idProdotto);
        bool prodottoExists(int id);
        //FRANCESCO
        Task<bool> DeleteOrdineAsync(int idOrdineEsistente);
      
    }
}
