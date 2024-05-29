using AcademyShopAPI.Models;
using DtoLayer.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class RepositoryGetOrdini : IRepositoryGetOrdini
    {
        private readonly AcademyShopDBContext _context;

        public RepositoryGetOrdini(AcademyShopDBContext context)
        {
            _context = context;
        }

        public async Task<OrdineDettaglioDTOperGET> GetOrdineDettaglioAsync(int userId, int dettaglioOrdineId)
        {
            var ordineDettaglio = await (from ordine in _context.Ordines
                                         join statoOrdine in _context.StatoOrdines on ordine.FkIdStato equals statoOrdine.Id
                                         join dettaglioOrdine in _context.DettaglioOrdines on ordine.Id equals dettaglioOrdine.FkIdOrdine
                                         join prodotto in _context.Prodottos on dettaglioOrdine.FkIdProdotto equals prodotto.Id
                                         where ordine.FkIdUtente == userId && dettaglioOrdine.Id == dettaglioOrdineId
                                         select new OrdineDettaglioDTOperGET
                                         {
                                             ProdottoId = prodotto.Id,
                                             ProdottoNome = prodotto.Nome,
                                             ProdottoDescrizione = prodotto.Descrizione,
                                             StatoOrdineDescrizione = statoOrdine.Descrizione,
                                             Quantita = dettaglioOrdine.Quantita,
                                             DataRegistrazione = ordine.DataRegistrazione,
                                             DataAggiornamento = ordine.DataAggiornamento
                                         }).FirstOrDefaultAsync();

            return ordineDettaglio;
        }

        public async Task<string?> GetUserPassword(int userId)
        {
            var user = await _context.Utentes.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.Password;
        }

        public async Task<bool> VerificaUserAsync(int userId, string password)
        {
            return await _context.Utentes.AnyAsync(u => u.Id == userId && u.Password == password);
        }
    }

}
