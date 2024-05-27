﻿using AcademyShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ManageData
    {
        private readonly AcademyShopDBContext _context;

        public ManageData(AcademyShopDBContext context)
        {
            _context = context;
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

    }
}
