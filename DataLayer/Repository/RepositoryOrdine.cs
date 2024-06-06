using AcademyShopAPI.Models;
using DtoLayer.Dto;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer.Repository
{
    public class RepositoryOrdine : IRepositoryOrdine
    {

        private readonly AcademyShopDBContext _context;

        public RepositoryOrdine(AcademyShopDBContext context)
        {
            _context = context;
        }

        //Renato
        public async Task<List<OrdineDettaglioDTOperGetALL>> GetOrdiniByUserId(int userId)
        {
            try
            {
                var ordini = await _context.Ordines
                    .Where(o => o.FkIdUtente == userId)
                    .Select(o => new OrdineDettaglioDTOperGetALL
                    {
                        ProdottoNome = o.DettaglioOrdines.First().FkIdProdottoNavigation.Nome,
                        DataRegistrazione = o.DataRegistrazione,
                        DataAggiornamento = o.DataAggiornamento,
                        StatoOrdineDescrizione = o.FkIdStatoNavigation.Descrizione,
                        ProdottoId = o.DettaglioOrdines.First().FkIdProdottoNavigation.Id,
                        ProdottoDescrizione = o.DettaglioOrdines.First().FkIdProdottoNavigation.Descrizione,
                        Quantita = o.DettaglioOrdines.First().Quantita,
                        IdDettaglioOrdine = o.DettaglioOrdines.First().Id,
                        IdOrdine = o.Id
                    })
                    .ToListAsync();

                return ordini; // Restituisce la lista degli ordini trovati.

            }
            catch (Exception ex)
            {
                // In caso di eccezione, solleva una nuova eccezione con un messaggio specifico.
                throw new Exception("Non ci sono ordini per questo utente", ex);
            }
        }
        public List<Ordine> GetOrdiniByUserIdSync(int userId)
        {
            return _context.Ordines.Where(ordine => ordine.FkIdUtente == userId).ToList();
        }
        //Gabriele
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
        //Damiano
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

        public async Task<bool> ModificaOrdineTransazioneAsync(Ordine ordine, DettaglioOrdine dettaglioOrdine, Prodotto prodotto, int quantita)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ordine.DataAggiornamento = DateTime.Now;
                    ordine.FkIdStato = 2;
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

        //Adriano
        public List<DettaglioOrdine> GetDettaglioOrdineSync(List<Ordine> ordineList)
        {
            return _context.DettaglioOrdines.Where(dettaglioOrdine => ordineList.Select(o => o.Id).Contains(dettaglioOrdine.FkIdOrdine)).ToList();
            }
        public async Task<int> addOrdine(int idUtente, Prodotto prodotto, int quantità)
        {
            Ordine ordine;
            DettaglioOrdine dettaglioOrdine;

            // Creazione ordine 
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {

                try
                {
                    ordine = new Ordine(idUtente, 1, DateTime.Now);
                    
                    _context.Ordines.Add(ordine);
                    await _context.SaveChangesAsync();

                    transactionScope.Complete();

                }
                catch (Exception)// codice errore 500
                {
                    transactionScope.Dispose();
                    throw new TransactionAbortedException();
                }
            }

            // aggiornamento quantità prodotto e creazione nuovo dettaglio ordine
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {

                try
                {
                    dettaglioOrdine = new DettaglioOrdine(ordine.Id, prodotto.Id, quantità);

                    _context.Entry(prodotto).State = EntityState.Modified;
                    _context.DettaglioOrdines.Add(dettaglioOrdine);

                    await _context.SaveChangesAsync();

                    transactionScope.Complete();
                    return ordine.Id;

                }
                catch (Exception)// codice errore 500
                {
                    transactionScope.Dispose();
                    //Cancellazione Ordine in caso di fallimento della seconda transazione
                    _context.Ordines.Remove(ordine);
                    await _context.SaveChangesAsync();
                    throw new TransactionException();
                }
            }
        }

        public async Task<Prodotto> getProdottoAsync(int idProdotto)
        {
            // Controllo esistenza del prodotto 
        #pragma warning disable CS8603
            return await _context.Prodottos.FindAsync(idProdotto);
        #pragma warning restore CS8603
        }

        public bool prodottoExists(int id)
        {
            return _context.Prodottos.Any(e => e.Id == id);
        }

        //Fancesco 
        public async Task<bool> DeleteOrdineAsync(int idOrdineEsistente)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Recupera l'ordine esistente
                    var ordine = await _context.Ordines.FindAsync(idOrdineEsistente);

                    // Recupera il dettaglio ordine esistente
                    var dettaglioOrdine = await _context.DettaglioOrdines
                        .FirstOrDefaultAsync(d => d.FkIdOrdine == idOrdineEsistente);
                    // Aggiorno le quantità dei prodotti
                    foreach (var dettaglio in ordine.DettaglioOrdines)
                    {
                        var prodotto = await _context.Prodottos.FirstOrDefaultAsync(p => p.Id == dettaglio.FkIdProdotto);
                        prodotto.Quantità += dettaglio.Quantita;  // Rimetto in stock i prodotti dell'ordine eliminato
                    }

                    // Elimino dettaglio ordine
                    _context.DettaglioOrdines.RemoveRange(ordine.DettaglioOrdines);

                    // Elimino l'ordine
                    _context.Ordines.Remove(ordine);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true; // Successo
                }

                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    // Gestione dell'errore
                    throw new Exception("Errore durante la cancellazione dell'ordine.", ex);
                }
            }
        }

    }

}

