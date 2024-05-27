using AcademyShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace DataLayer
{
    public class ManageData
    {
        private readonly AcademyShopDBContext _context;

        public ManageData(AcademyShopDBContext context)
        {
            _context = context;
        }

        //Damiano
        public async Task<int?> RecuperaIdOrdineAsync(int idUtente, int idDettaglioOrdine)
        {
            try
            {
                // Esegui una query per recuperare l'ID dell'ordine
                var ordine = await _context.DettaglioOrdines
               .Where(dettaglio => dettaglio.Id == idDettaglioOrdine)
               .Join(_context.Ordines,
                     dettaglio => dettaglio.FkIdOrdine,
                     ordine => ordine.Id,
                     (dettaglio, ordine) => ordine)
               .Where(ordine => ordine.FkIdUtente == idUtente)
               .FirstOrDefaultAsync();

                if (ordine != null)
                {
                    // Restituisci l'ID dell'ordine trovato
                    return ordine.Id;
                }
                else
                {
                    // L'ordine non è stato trovato
                    return null;
                }
            }

            catch (Exception ex)
            {
                // Gestisci l'errore, ad esempio registrandolo o sollevando un'eccezione
                // Qui puoi anche restituire null o un altro valore per indicare un errore
                throw new Exception("Errore durante il recupero dell'ID dell'ordine.", ex);
            }
        }

        public async Task<int?> RecuperaStatoOrdineAsync(int idOrdineEsistente)
        {
            try
            {
                // Esegui una query per recuperare l'ordine con lo stato desiderato
                var ordine = await _context.Ordines
                    .Where(o => o.Id == idOrdineEsistente)
                    .FirstOrDefaultAsync();

                if (ordine != null)
                {
                    // Restituisci l'ID dell'ordine trovato
                    return ordine.FkIdStato;
                }
                else
                {
                    // L'ordine con lo stato specificato non è stato trovato
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Gestisci l'errore, ad esempio registrandolo o sollevando un'eccezione
                // Qui puoi anche restituire null o un altro valore per indicare un errore
                throw new Exception("Errore durante il recupero dell'ID dell'ordine basato sullo stato.", ex);
            }
        }

        public async Task<int?> RecuperaQuantitaProdottoAsync(int idOrdineEsistente)
        {
            try
            {
                // Esegui una query per recuperare la quantità del prodotto
                var prodotto = await _context.DettaglioOrdines
                    .Where(p => p.FkIdOrdine == idOrdineEsistente)
                    .FirstOrDefaultAsync();

                if (prodotto != null)
                {
                    // Restituisci la quantità del prodotto trovato
                    return prodotto.Quantita;
                }
                else
                {
                    // Il prodotto non è stato trovato
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Gestisci l'errore, ad esempio registrandolo o sollevando un'eccezione
                // Qui puoi anche restituire null o un altro valore per indicare un errore
                throw new Exception("Errore durante il recupero della quantità del prodotto.", ex);
            }
        }

        public async Task<int?> RecuperaIdProdottoAsync(int idOrdineEsistente)
        {
            try
            {
                // Esegui una query per recuperare la quantità del prodotto
                var prodotto = await _context.DettaglioOrdines
                    .Where(p => p.FkIdOrdine == idOrdineEsistente)
                    .FirstOrDefaultAsync();

                if (prodotto != null)
                {
                    // Restituisci la quantità del prodotto trovato
                    return prodotto.FkIdProdotto;
                }
                else
                {
                    // Il prodotto non è stato trovato
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Gestisci l'errore, ad esempio registrandolo o sollevando un'eccezione
                // Qui puoi anche restituire null o un altro valore per indicare un errore
                throw new Exception("Errore durante il recupero della quantità del prodotto.", ex);
            }
        }

        public async Task<bool> ModificaOrdineAsync(int idOrdineEsistente, int idProdotto, int quantita)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Recupera l'ordine esistente
                    var ordine = await _context.Ordines.FindAsync(idOrdineEsistente);
                    if (ordine == null)
                    {
                        throw new Exception("Ordine non trovato.");
                    }

                    // Aggiorna la data di aggiornamento e lo stato dell'ordine
                    ordine.DataAggiornamento = DateTime.Now;
                    ordine.FkIdStato = 2; // Assumendo che 2 sia l'ID dello stato "AGGIORNATO"
                    _context.Entry(ordine).State = EntityState.Modified;

                    // Recupera il dettaglio ordine esistente
                    var dettaglioOrdine = await _context.DettaglioOrdines
                        .FirstOrDefaultAsync(d => d.FkIdOrdine == idOrdineEsistente);

                    if (dettaglioOrdine != null)
                    {
                        dettaglioOrdine.Quantita -= quantita;
                        _context.Entry(dettaglioOrdine).State = EntityState.Modified;

                        // Aggiorna la quantità del prodotto nel magazzino
                        var prodotto = await _context.Prodottos.FindAsync(idProdotto);
                        if (prodotto != null)
                        {
                            prodotto.Quantità -= quantita;
                            _context.Entry(prodotto).State = EntityState.Modified;
                        }
                        else
                        {
                            throw new Exception("Prodotto non trovato.");
                        }
                    }
                    else
                    {
                        throw new Exception("Dettaglio ordine non trovato.");
                    }

                    // Salva tutte le modifiche nel contesto
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true; // Operazione completata con successo
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    // Gestione dell'errore
                    throw new Exception("Errore durante la modifica dell'ordine.", ex);
                }
            }
        }
        //Francesco
        public async Task<bool> DeleteOrdineAsync(int idOrdineEsistente)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Recupera l'ordine esistente
                    var ordine = await _context.Ordines.FindAsync(idOrdineEsistente);
                    if (ordine == null)
                    {
                        throw new Exception("Ordine non trovato.");
                    }



                    // Recupera il dettaglio ordine esistente
                    var dettaglioOrdine = await _context.DettaglioOrdines
                        .FirstOrDefaultAsync(d => d.FkIdOrdine == idOrdineEsistente);
                    // Aggiorno le quantità dei prodotti
                    foreach (var dettaglio in ordine.DettaglioOrdines)
                    {
                        var prodotto = await _context.Prodottos.FirstOrDefaultAsync(p => p.Id == dettaglio.FkIdProdotto);
                        if (prodotto != null)
                        {
                            prodotto.Quantità += dettaglio.Quantita;  // Rimetto in stock i prodotti dell'ordine eliminato
                        }
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
                    throw new Exception("Errore durante la modifica dell'ordine.", ex);
                }
            }
        }
        //Gabriele
        public async Task<OrdineDettaglioDTOperGET> GetOrdineDettaglioAsync(int userId, int dettaglioOrdineId)
        {
#pragma warning disable CS8603
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
#pragma warning restore CS8603 
        }

        //Daniel
        public async Task<IEnumerable<Utente>> GetUtentes()
        {
            try
            {
                return await _context.Utentes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero degli utenti.", ex);
            }
        }

        public async Task<Utente> GetUtente(int id)
        {
            try
            {
                return await _context.Utentes.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante il recupero dell'utente con ID {id}.", ex);
            }
        }

        public async Task<ActionResult<Utente>> PostUtente(Utente utente)
        {
            try
            {
                if (await _context.Utentes.AnyAsync(u => u.CodiceFiscale == utente.CodiceFiscale || u.Email == utente.Email))
                {
                    return new ContentResult
                    {
                        Content = "Un utente con lo stesso codice fiscale o email esiste già.",
                        ContentType = "text/plain",
                        StatusCode = 409
                    };
                }

                utente.DataRegistrazione = DateTime.UtcNow;

                _context.Utentes.Add(utente);
                await _context.SaveChangesAsync();

                return new CreatedAtRouteResult(nameof(GetUtente), new { id = utente.Id }, utente);
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la creazione dell'utente.", ex);
            }
        }

        public async Task<string> DeleteUtente(int id)
        {
            try
            {
                var utente = await _context.Utentes.FindAsync(id);
                if (utente == null)
                {
                    return "Utente non trovato.";
                }

                _context.Utentes.Remove(utente);
                await _context.SaveChangesAsync();

                return $"Utente '{utente.Nome} {utente.Cognome}' eliminato con successo.";
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante l'eliminazione dell'utente con ID {id}.", ex);
            }
        }
        public async Task<Prodotto> GetProdottoAsync(int id)
        {
#pragma warning disable CS8603 // Possibile restituzione di riferimento Null.
            return await _context.Prodottos.FindAsync(id);
#pragma warning restore CS8603 // Possibile restituzione di riferimento Null.
        }
        public async Task<int> NuovoOrdine(int idUtente, Prodotto prodotto, int quantità)
        {
            int idOrdine;
            Ordine ordine = new Ordine();

            var dettaglioOrdine = new DettaglioOrdine();

            // Creazione ordine 
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {

                try
                {

                    ordine.FkIdUtente = idUtente;
                    ordine.FkIdStato = 1;
                    ordine.DataRegistrazione = DateTime.Now;

                    _context.Ordines.Add(ordine);

                    await _context.SaveChangesAsync();

                    transactionScope.Complete();

                }
                catch (Exception ex)
                {// codice errore 500
                    transactionScope.Dispose();
                    throw new TransactionAbortedException();
                }
            }

            // aggiornamento quantità prodotto e creazione nuovo dettaglio ordine
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {

                try
                {
                    dettaglioOrdine.FkIdOrdine = ordine.Id;
                    dettaglioOrdine.FkIdProdotto = prodotto.Id;
                    dettaglioOrdine.Quantita = quantità;

                    _context.Entry(prodotto).State = EntityState.Modified;
                    _context.DettaglioOrdines.Add(dettaglioOrdine);

                    await _context.SaveChangesAsync();

                    transactionScope.Complete();
                    return ordine.Id;

                }
                catch (Exception ex)
                {// codice errore 500
                    transactionScope.Dispose();
                    throw new TransactionException();
                }
            }


        }
    }
}
