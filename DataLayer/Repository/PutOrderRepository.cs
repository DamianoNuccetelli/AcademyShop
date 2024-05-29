using AcademyShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class PutOrderRepository : IPutOrderRepository

    {
        private readonly AcademyShopDBContext _context;

        public PutOrderRepository(AcademyShopDBContext context)
        {
            _context = context;
        }

        public async Task<Ordine?> RecuperaOrdineAsync(int idOrdineEsistente)
        {
            return await _context.Ordines.FindAsync(idOrdineEsistente);
        }

        public async Task<DettaglioOrdine?> RecuperaDettaglioOrdineAsync(int idDettaglioOrdine)
        {
            return await _context.DettaglioOrdines
                .FirstOrDefaultAsync(d => d.Id == idDettaglioOrdine);
        }

        public async Task<Prodotto?> RecuperaProdottoAsync(int idProdotto)
        {
            return await _context.Prodottos.FindAsync(idProdotto);
        }

        public async Task<int?> RecuperaIdOrdineAsync(int idUtente, int idDettaglioOrdine)
        {
            var ordine = await _context.DettaglioOrdines
                .Where(dettaglio => dettaglio.Id == idDettaglioOrdine)
                .Join(_context.Ordines,
                      dettaglio => dettaglio.FkIdOrdine,
                      ordine => ordine.Id,
                      (dettaglio, ordine) => ordine)
                .Where(ordine => ordine.FkIdUtente == idUtente)
                .FirstOrDefaultAsync();

            return ordine?.Id;
        }

        public async Task<int?> RecuperaStatoOrdineAsync(int idOrdineEsistente)
        {
            var ordine = await _context.Ordines
                .Where(o => o.Id == idOrdineEsistente)
                .FirstOrDefaultAsync();

            return ordine?.FkIdStato;
        }

        public async Task<int?> RecuperaIdProdottoAsync(int idOrdineEsistente)
        {
            var prodotto = await _context.DettaglioOrdines
                .Where(p => p.FkIdOrdine == idOrdineEsistente)
                .FirstOrDefaultAsync();

            return prodotto?.FkIdProdotto;
        }

        public async Task<int?> RecuperaQuantitaProdottoAsync(int idProdotto)
        {
            var prodotto = await _context.Prodottos.FindAsync(idProdotto);
            return prodotto?.Quantità;
        }

        public async Task<bool> ModificaOrdineTransazioneAsync(Ordine ordine, DettaglioOrdine dettaglioOrdine, Prodotto prodotto, int statoOrdine, int quantita)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ordine.DataAggiornamento = DateTime.Now;
                    ordine.FkIdStato = statoOrdine;
                    dettaglioOrdine.Quantita += quantita;
                    prodotto.Quantità -= quantita;

                    _context.Entry(ordine).State = EntityState.Modified;
                    _context.Entry(dettaglioOrdine).State = EntityState.Modified;
                    _context.Entry(prodotto).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<Ordine?> RecuperaOrdineModificatoAsync(int idOrdine)
        {
            return await _context.Ordines
                .Include(o => o.DettaglioOrdines)
                    .ThenInclude(d => d.FkIdProdottoNavigation)
                .Include(o => o.FkIdStatoNavigation)
                .FirstOrDefaultAsync(o => o.Id == idOrdine);
        }
    }


}

