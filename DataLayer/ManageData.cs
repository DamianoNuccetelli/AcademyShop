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
using Azure.Core;
using Microsoft.AspNetCore.Http;

namespace DataLayer
{
    public class ManageData
    {
        private readonly AcademyShopDBContext _context;

        public ManageData(AcademyShopDBContext context)
        {
            _context = context;
        }

        //Metodo  per verificare se utente è presente Florea Renato
        public async Task<int?> UtenteExists(int id)
        {
            var utente = await _context.Utentes.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (utente != null)
            {
                return utente.Id;
            }

            else
            {
                return null;
            }

        }
        //Florea Renato operazione per ottenere i dati degli ordini ricevendo in input l'id utente 
        public async Task<List<OrdiniByIdUserDTO>> GetOrdiniByUserId(int userId)
        {
            try
            {

                //var ordini = await _context.Ordines
                //    .Where(o => o.FkIdUtente == userId)
                //    .Select(o => new OrdiniByIdUserDTO
                //    {
                //        DataRegistrazione = o.DataRegistrazione,
                //        DataAggiornamento = o.DataAggiornamento,
                //        DescrizioneStato = o.FkIdStatoNavigation.Descrizione, 
                //        IDProdotto = o.DettaglioOrdines.First().FkIdProdottoNavigation.Id,
                //        DescrizioneProdotto = o.DettaglioOrdines.First().FkIdProdottoNavigation.Descrizione,
                //        Quantita = o.DettaglioOrdines.First().Quantita
                //    })
                //    .ToListAsync();
                var ordini = await (from o in _context.Ordines
                                    join s in _context.StatoOrdines on o.FkIdStato equals s.Id
                                    join d in _context.DettaglioOrdines on o.Id equals d.FkIdOrdine
                                    join p in _context.Prodottos on d.FkIdProdotto equals p.Id
                                    where o.FkIdUtente == userId
                                    select new OrdiniByIdUserDTO
                                    {
                                        DataRegistrazione = o.DataRegistrazione,
                                        DataAggiornamento = o.DataAggiornamento,
                                        DescrizioneStato = s.Descrizione,
                                        IDProdotto = p.Id,
                                        DescrizioneProdotto = p.Descrizione,
                                        Quantita = d.Quantita
                                    }).ToListAsync();

                return ordini;

            }
            catch (Exception ex)
            {
                throw new Exception("Non ci sono ordini per questo utente", ex);

            }
        }
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
                    // Restituisci l'id del prodotto trovato
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

        public async Task<int?> RecuperaQuantitaProdottoAsync(int idProdotto)
        {
            try
            {
                // Esegui una query per recuperare la quantità del prodotto
                var prodotto = await _context.Prodottos.FindAsync(idProdotto);
                if (prodotto != null)
                {
                    return prodotto.Quantità;
                }
                else
                {
                    throw new Exception("Prodotto non trovato.");
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
                        dettaglioOrdine.Quantita += quantita;
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

        public async Task<Ordine> RecuperaOrdineModificatoAsync(int idOrdine)
        {
            try
            {
                var ordine = await _context.Ordines
                    .Include(o => o.DettaglioOrdines)
                        .ThenInclude(d => d.FkIdProdottoNavigation)
                    .Include(o => o.FkIdStatoNavigation)
                    .FirstOrDefaultAsync(o => o.Id == idOrdine);

                if (ordine == null)
                {
                    throw new Exception("Ordine non trovato.");
                }

                return ordine;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'ordine.", ex);
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

            var ordineDettaglio = await (from ordine in _context.Ordines
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

            return ordineDettaglio;
        }
        //Gabriele
        public async Task<string?> GetUserPassword(int userId)
        {
            var user = await _context.Utentes.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.Password;
        }
        //Gabriele
        public async Task<bool> VerificaUserAsync(int userId, string password)
        {
            return await _context.Utentes.AnyAsync(u => u.Id == userId && u.Password == password);
        }



        //Daniel -> Aggiunta e rimozione dell'utente dal db
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
                //Verifica dell'esistenza dell'utente:
                if (await _context.Utentes.AnyAsync(u => u.CodiceFiscale == utente.CodiceFiscale || u.Email == utente.Email))
                {
                    return new ObjectResult("Un utente con lo stesso codice fiscale o email esiste già.")
                    {
                        StatusCode = StatusCodes.Status409Conflict
                    };
                }

                //Imposto la data di registrazione a quella attuale
                utente.DataRegistrazione = DateTime.UtcNow;

                //Aggiunta dell'utente al contesto
                _context.Utentes.Add(utente);
                await _context.SaveChangesAsync();

                //Restituzione della risposta di creazione
                return new CreatedAtRouteResult(nameof(GetUtente), new { id = utente.Id }, utente);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Errore durante la creazione dell'utente. Problema con il database.", dbEx);
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
                //Cerca l'utente con l'ID specificato
                var utente = await _context.Utentes.FindAsync(id);
                if (utente == null)
                {
                    return "Utente non trovato.";
                }

                _context.Utentes.Remove(utente);
                await _context.SaveChangesAsync();

                // Restituisce un messaggio di conferma dell'eliminazione dell'utente
                return $"Utente '{utente.Nome} {utente.Cognome}' eliminato con successo.";
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Errore durante l'eliminazione dell'utente con ID {id}. Problema con il database.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante l'eliminazione dell'utente con ID {id}.", ex);
            }
        }

        //Adriano
      
        public async Task<int> NuovoOrdine(int idUtente, Prodotto prodotto, int quantità)
        {
            Ordine ordine = new();
            DettaglioOrdine dettaglioOrdine = new();

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
                catch (Exception)
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
                catch (Exception)
                {// codice errore 500
                    transactionScope.Dispose();
                    throw new TransactionException();
                }
            }
           
        }
        public async Task<Prodotto> GetProdottoAsync(int idProdotto)
        {    // Controllo esistenza del prodotto 
            if (ProdottoExists(idProdotto))
            {
                #pragma warning disable CS8603
                return await _context.Prodottos.FindAsync(idProdotto);
                #pragma warning restore CS8603
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
        public bool ProdottoExists(int id)
        {
            return _context.Prodottos.Any(e => e.Id == id);
        }
    }
}
