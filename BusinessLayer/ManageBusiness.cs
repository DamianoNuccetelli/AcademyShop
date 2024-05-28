﻿using AcademyShopAPI.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;


namespace BusinessLayer
{
    public class ManageBusiness
    {

        private readonly ManageData oDL;

        public ManageBusiness(ManageData _oDL)
        {
            oDL = _oDL;
        }


        private OrdineModificatoDTO MapToDTO(Ordine ordine) 
        {
            return new OrdineModificatoDTO
            {
                Id = ordine.Id,
                FkIdUtente = ordine.FkIdUtente,
                FkIdStato = ordine.FkIdStato,
                DataRegistrazione = ordine.DataRegistrazione,
                DataAggiornamento = ordine.DataAggiornamento
               
            };
        }
        //Renato Florea; chiamata al dataLayer
        public async Task<int?> UtenteExists(int id)
        {
            try
            {
                return await oDL.UtenteExists(id);
            }

            catch (Exception ex)
            {
                throw new Exception("errore durante recupero id utente nel business layer", ex);
            }
        }


        //Renato Florea chiamata al DataLayer
        public async Task<List<OrdineDettaglioDTOperGET>> GetOrdiniByUserId(int userId)
        {
            try
            {
                // Verifica se l'utente esiste
                bool utenteExists = await oDL.CheckUtenteExistsById(userId);

                if (!utenteExists)
                {
                    // Se l'utente non esiste, restituisci una risposta di errore 400 (Bad Request)
                    throw new ArgumentException();
                }

                // Chiama il metodo corrispondente del data layer per recuperare gli ordini dell'utente
                var ordini = await oDL.GetOrdiniByUserId(userId);

                if (ordini.Count == 0)
                {
                    // Se non ci sono ordini, restituisci un messaggio appropriato
                    throw new InvalidOperationException();
                }

                // Se ci sono ordini, restituisci gli ordini dell'utente
                return ordini;
            }
            catch (InvalidOperationException o)
            {
                throw new Exception("Non ci sono ordini per questo utente", o);

            }
            catch (ArgumentException u)
            {
                throw new Exception("Utente non trovato", u);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui 
                throw new Exception("Errore durante il recupero degli ordini dell'utente.", ex);
            }
        }


        public async Task<(bool success, string message, int statusCode, OrdineModificatoDTO? ordineModificato)> ModificaOrdineCompletaAsync(int idUtente, int idDettaglioOrdine, int quantita)
        {
            // Verifica l'esistenza dell'ordine
            int? idOrdineEsistente = await oDL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);
            if (idOrdineEsistente == null)
            {
                return (false, "L'ordine non esiste.", 404, null);
            }

            // Recupera lo stato dell'ordine
            int statoOrdine = (int)await oDL.RecuperaStatoOrdineAsync((int)idOrdineEsistente);
            if (statoOrdine == 3)
            {
                return (false, "L'ordine è chiuso.", 400, null);
            }

            // Recupera l'ID del prodotto
            int? idProdotto = await oDL.RecuperaIdProdottoAsync((int)idOrdineEsistente);
            if (idProdotto == null)
            {
                return (false, "Il prodotto non esiste.", 404, null);
            }

            // Recupera la quantità del prodotto
            int? quantitaProdottoDisponibile = await oDL.RecuperaQuantitaProdottoAsync((int)idProdotto);
            if (quantitaProdottoDisponibile <= quantita || quantitaProdottoDisponibile == 0)
            {
                return (false, "La quantità disponibile non è sufficiente.", 400, null);
            }

            // Modifica l'ordine
            bool successo = await oDL.ModificaOrdineAsync((int)idOrdineEsistente, (int)idProdotto, quantita);
            if (!successo)
            {
                return (false, "Errore nell'operazione di modifica dell'ordine.", 500, null);
            }

            var ordineModificato = await oDL.RecuperaOrdineModificatoAsync((int)idOrdineEsistente);
            var ordineModificatoDTO = MapToDTO(ordineModificato); // Mappa l'entità dell'ordine modificato a DTO

            return (true, string.Empty, 204, ordineModificatoDTO);
        }


        public async Task<int?> RecuperaIdOrdineAsync(int idUtente, int idDettaglioOrdine)
        {
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'ID dell'ordine nel business layer.", ex);
            }
        }


        public async Task<int?> RecuperaStatoOrdineAsync(int idOrdineEsistente)
        {
           // oDL = new ManageData(_context);
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaStatoOrdineAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'ID dello stato dell'ordine nel business layer.", ex);
            }
        }

        public async Task<int?> RecuperaIdProdottoAsync(int idOrdineEsistente)
        {
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID del prodotto 
                return await oDL.RecuperaIdProdottoAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'ID del prodotto nel business layer.", ex);
            }
        }

        public async Task<int?> RecuperaQuantitaProdottoAsync(int idOrdineEsistente)
        {
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare la quantità del prodotto
                return await oDL.RecuperaQuantitaProdottoAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero della quantita del prodotto nel business layer.", ex);
            }
        }


        public async Task<bool> ModificaOrdineAsync(int idOrdineEsistente, int idProdotto, int quantita)
        {
            try
            {
                return await oDL.ModificaOrdineAsync(idOrdineEsistente, idProdotto, quantita);
            }
            catch (Exception ex)
            {
                // Gestisci l'errore, ad esempio registrandolo o rilanciandolo
                throw new Exception("Errore durante la modifica dell'ordine nel business layer.", ex);
            }
        }

        public async Task<bool> DeleteOrdineAsync(int idOrdineEsistente)
        {
            try
            {
                return await oDL.DeleteOrdineAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la cancellazione dell'ordine nel business layer.", ex);
            }
        }

        //Gabriele

        public async Task<OrdineDettaglioDTOperGET> GetOrdineDettaglioAsync(int userId, int dettaglioOrdineId)
        {
            ContentResult result = new ContentResult();
            try
            {
                // Recupera i dettagli dell'ordine
                if (dettaglioOrdineId <= 0)
                {
                    throw new ArgumentException("L'ID dell'ordine non è valido.");
                   
                }
                if(oDL.DettaglioOrdineExists(dettaglioOrdineId) == false)
                {
                    throw new ArgumentException("Dettaglio ordine non trovato.");
                }  

                bool utenteExists = await oDL.CheckUtenteExistsById(userId);

                if (utenteExists == false)
                {
                    throw new ArgumentException("Utente non trovato");
                }
                return await oDL.GetOrdineDettaglioAsync(userId, dettaglioOrdineId);
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nell'esecuzione del programma: ----> " + ex.Message);
            }
        }
       
        //Daniel -> Aggiunta e rimozione dell'utente dal db
        public async Task<IEnumerable<Utente>> GetUtentes()
        {
            return await oDL.GetUtentes();
        }

        public async Task<Utente> GetUtente(int id)
        {
            return await oDL.GetUtente(id);
        }

        public async Task<ActionResult<Utente>> PostUtente(Utente utente)
        {
            try
            {
                // Verifico se un utente è gia esistente
                if (await oDL.CheckUtenteExists(utente))
                {
                    return ErrorContentResult("Un utente con lo stesso codice fiscale o email esiste già.", 409);
                }

                // Controllo tramite regex del codice fiscale
                if (!IsValidCodiceFiscale(utente.CodiceFiscale))
                {
                    return ErrorContentResult("Un utente con lo stesso codice fiscale o email esiste già.");
                }

                // Controllo tramite regex dell'email
                if (!IsValidEmail(utente.Email))
                {
                    return ErrorContentResult("L'email non è valida.");
                }

                // Controllo sulla data di nascita (l'utente deve essere nato tra il 1900 e il giorno d'oggi)
                if (!IsValidBirthDate(utente.DataNascita))
                {
                    return ErrorContentResult("La data di nascita non è valida.");
                }


                if (utente.CodiceFiscale.Length != 16 || utente.Password.Length != 16 || utente.ProvinciaNascita.Length != 2 || (utente.Sesso != "M" && utente.Sesso != "F"))
                {
                    return ErrorContentResult("Uno o più campi non rispettano la lunghezza o i valori richiesti.");
                }

                return await oDL.PostUtente(utente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante la creazione dell'utente: {ex.Message}");
            }
        }

        public async Task<ActionResult<Utente>> DeleteUtente(int id)
        {
            try
            {
                var utente = await oDL.GetUtente(id);
                if (utente == null)
                {
                    return new NotFoundResult();
                }

                var result = await oDL.DeleteUtente(id);

                //return Ok($"Utente '{utente.Nome} {utente.Cognome}' eliminato con successo.");
                return new OkObjectResult($"Utente '{utente.Nome} {utente.Cognome}' eliminato con successo.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante l'eliminazione dell'utente con ID {id} nel livello della logica di business.", ex);
            }
        }

        private bool IsValidCodiceFiscale(string codiceFiscale)
        {
            string regexPattern = @"^[A-Z]{6}\d{2}[ABCDEHLMPRST]\d{2}[A-Z]\d{3}[A-Z]$";
            return System.Text.RegularExpressions.Regex.IsMatch(codiceFiscale, regexPattern);
        }

        private bool IsValidEmail(string email)
        {
            string regexPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, regexPattern);
        }

        private bool IsValidBirthDate(DateOnly birthDate)
        {
            DateOnly minDate = new DateOnly(1900, 1, 1);
            DateOnly maxDate = DateOnly.FromDateTime(DateTime.UtcNow);
            return birthDate >= minDate && birthDate <= maxDate;
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


        //Adriano
        public async Task<int> nuovoOrdine(int idUtente, int idProdotto, int quantità)
        {
            Prodotto prodotto;
            if (oDL.prodottoExists(idProdotto))
            {
                prodotto = await oDL.getProdottoAsync(idProdotto);
            }
            else
            {
                throw new KeyNotFoundException();
            }

            if (prodotto != null && quantità != 0
                && prodotto.Quantità >= quantità)
            {
                prodotto.Quantità -= quantità;

                int idOrdine = await oDL.nuovoOrdine(idUtente, prodotto, quantità);
                return idOrdine;
            }
            else
            {// codice errore 400
                throw new ArgumentException();
            }
        }
        }
    }

