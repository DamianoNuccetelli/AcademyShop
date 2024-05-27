using AcademyShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoLayer.Dto;

namespace DataLayer
{
    public class ManageData
    {
        private readonly AcademyShopDBContext _context;

        public ManageData(AcademyShopDBContext context)
        {
            _context = context;
        }
        //Gabriele
        public async Task<OrdineDettaglioDTOperGET> GetOrdineDettaglioAsync(int userId, int dettaglioOrdineId)
        {
#pragma warning disable CS8603 // Possibile restituzione di riferimento Null.
            return await (from ordine in _context.Ordines
                          join statoOrdine in _context.StatoOrdines on ordine.FkIdStato equals statoOrdine.Id
                          join dettaglioOrdine in _context.DettaglioOrdines on ordine.Id equals dettaglioOrdine.FkIdOrdine
                          join prodotto in _context.Prodottos on dettaglioOrdine.FkIdProdotto equals prodotto.Id
                          where ordine.FkIdUtente == userId && dettaglioOrdine.Id == dettaglioOrdineId
                          select new OrdineDettaglioDTOperGET
                          {
                              ProdottoNome = prodotto.Nome,
                              ProdottoDescrizione = prodotto.Descrizione,
                              StatoOrdineDescrizione = statoOrdine.Descrizione,
                              Quantita = dettaglioOrdine.Quantita,
                              ProdottoId = prodotto.Id,
                              DataRegistrazione = ordine.DataRegistrazione,
                              DataAggiornamento = ordine.DataAggiornamento
                          }).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possibile restituzione di riferimento Null.
        }
    }
}
