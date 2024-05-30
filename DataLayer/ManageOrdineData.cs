﻿using AcademyShopAPI.Models;
using DataLayer.Repository;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer
{
    public class ManageOrdineData
    {
        private readonly AcademyShopDBContext _context;
        private readonly IRepositoryAsync<Ordine> repo;
        private readonly IRepositoryOrdine _repositoryOrdine;

        public ManageOrdineData(IRepositoryOrdine repositoryOrdine, AcademyShopDBContext _academyShopDBContext, IRepositoryAsync<Ordine> _repo)
        {
           _repositoryOrdine = repositoryOrdine;
            repo = _repo;
            _context = _academyShopDBContext;
        }


        //Florea Renato operazione per ottenere i dati degli ordini ricevendo in input l'id utente 
        public async Task<List<OrdineDettaglioDTOperGET>> GetOrdiniByUserId(int userId)
        {
            return await _repositoryOrdine.GetOrdiniByUserId(userId);
        }

        //-----------------DAMIANO-----------------------//

        public async Task<Ordine?> RecuperaOrdineAsync(int idOrdineEsistente)
        {
            return await _repositoryOrdine.RecuperaOrdineAsync(idOrdineEsistente);
        }

        public async Task<DettaglioOrdine?> RecuperaDettaglioOrdineAsync(int idDettaglioOrdine)
        {
            return await _repositoryOrdine.RecuperaDettaglioOrdineAsync(idDettaglioOrdine);
        }

        public async Task<Prodotto?> RecuperaProdottoAsync(int idProdotto)
        {
            return await _repositoryOrdine.RecuperaProdottoAsync(idProdotto);
        }


        public async Task<int?> RecuperaIdOrdineAsync(int idUtente, int idDettaglioOrdine)
        {
            return await _repositoryOrdine.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);
        }

        public async Task<int?> RecuperaStatoOrdineAsync(int idOrdineEsistente)
        {
           return await _repositoryOrdine.RecuperaStatoOrdineAsync(idOrdineEsistente);
        }

        public async Task<int?> RecuperaIdProdottoAsync(int idOrdineEsistente)
        {
           return await _repositoryOrdine.RecuperaIdProdottoAsync(idOrdineEsistente);
        }

        public async Task<int?> RecuperaQuantitaProdottoAsync(int idProdotto)
        {
            return await _repositoryOrdine.RecuperaQuantitaProdottoAsync(idProdotto);
        }


        public async Task<bool> ModificaOrdineTransazioneAsync(Ordine ordine, DettaglioOrdine dettaglioOrdine, Prodotto prodotto, int statoOrdine, int quantita)
        {
           return await _repositoryOrdine.ModificaOrdineTransazioneAsync(ordine, dettaglioOrdine, prodotto, statoOrdine, quantita);
        }

        public async Task<Ordine?> RecuperaOrdineModificatoAsync(int idOrdine)
        {
           return await _repositoryOrdine.RecuperaOrdineModificatoAsync(idOrdine);
        }

        //---------------------------------------------------------------------------------
        //Francesco
        public async Task<bool> DeleteOrdineAsync(int idOrdineEsistente)
        {
            return await _repositoryOrdine.DeleteOrdineAsync(idOrdineEsistente);
        }
        //Gabriele
        public async Task<OrdineDettaglioDTOperGET?> GetOrdineDettaglioAsync(int userId, int idDettaglioOrdine)
        {
            return await _repositoryOrdine.GetOrdineDettaglioAsync(userId, idDettaglioOrdine);
        }

        public async Task<string?> RecuperaPasswordUtenteAsync(int userId)
        {
            return await _repositoryOrdine.GetUserPassword(userId);
        }

        public async Task<bool> VerificaUtenteAsync(int userId, string password)
        {
            return await _repositoryOrdine.VerificaUserAsync(userId, password);
        }
        //Fine Gabriele



        public async Task<int> nuovoOrdine(int idUtente, Prodotto prodotto, int quantità)
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
                    dettaglioOrdine.FkIdOrdine = ordine.Id;
                    dettaglioOrdine.FkIdProdotto = prodotto.Id;
                    dettaglioOrdine.Quantita = quantità;

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
        {    // Controllo esistenza del prodotto 
#pragma warning disable CS8603
            return await _context.Prodottos.FindAsync(idProdotto);
#pragma warning restore CS8603
        }
        public bool prodottoExists(int id)
        {
            return _context.Prodottos.Any(e => e.Id == id);
        }
        public bool DettaglioOrdineExists(int id)
        {
            return _context.DettaglioOrdines.Any(e => e.Id == id);
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

        private ContentResult ErrorContentResult(string errorMessage, int statusCode = 400)
        {
            return new ContentResult
            {
                Content = errorMessage,
                ContentType = "text/plain",
                StatusCode = statusCode
            };
        }



    }
}