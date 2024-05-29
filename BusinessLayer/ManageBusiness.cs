using AcademyShopAPI.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Transactions;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;


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
               
                StatoOrdine = ordine.FkIdStato,
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

        //--------------------------------------DAMIANO----------------------------------------------------------
        public async Task<(bool success, string message, int statusCode, OrdineModificatoDTO? ordineModificato)> ModificaOrdineCompletaAsync(int idUtente, int idDettaglioOrdine, int quantita)
        {
            var idOrdineEsistente = await oDL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);
            if (idOrdineEsistente == null)
            {
                return (false, "L'ordine non esiste.", 404, null);
            }

            var statoOrdine = await oDL.RecuperaStatoOrdineAsync((int)idOrdineEsistente);
            if (statoOrdine == 3)
            {
                return (false, "L'ordine è chiuso.", 400, null);
            }

            var idProdotto = await oDL.RecuperaIdProdottoAsync((int)idOrdineEsistente);
            if (idProdotto == null)
            {
                return (false, "Il prodotto non esiste.", 404, null);
            }

            var quantitaProdottoDisponibile = await oDL.RecuperaQuantitaProdottoAsync((int)idProdotto);
            if (quantitaProdottoDisponibile <= quantita || quantitaProdottoDisponibile == 0)
            {
                return (false, "La quantità disponibile non è sufficiente.", 400, null);
            }

            // entità
            var ordine = await oDL.RecuperaOrdineAsync((int)idOrdineEsistente);
            var dettaglioOrdine = await oDL.RecuperaDettaglioOrdineAsync((int)idDettaglioOrdine);
            var prodotto = await oDL.RecuperaProdottoAsync((int)idProdotto);

            if (ordine == null || dettaglioOrdine == null || prodotto == null)
            {
                return (false, "Errore nel recupero dei dati.", 500, null);
            }


            // esecuzione transazione data layer
            var success = await oDL.ModificaOrdineTransazioneAsync(ordine, dettaglioOrdine, prodotto, (int)statoOrdine, quantita);
            if (!success)
            {
                return (false, "Errore nell'operazione di modifica dell'ordine.", 500, null);
            }

            var ordineModificato = await oDL.RecuperaOrdineModificatoAsync((int)idOrdineEsistente);
            var ordineModificatoDTO = MapToDTO(ordineModificato);

            return (true, string.Empty, 200, ordineModificatoDTO);
        }

        public async Task<Ordine?> RecuperaOrdineAsync(int idOrdineEsistente)
        {
            try
            {
                return await oDL.RecuperaOrdineAsync(idOrdineEsistente);
            }

            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'entità Ordine nel business layer.", ex);
            }
        }

        public async Task<DettaglioOrdine?> RecuperaDettaglioOrdineAsync(int idDettaglioOrdine)
        {

            try
            {
                return await oDL.RecuperaDettaglioOrdineAsync(idDettaglioOrdine);
            }

            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'entità DettaglioOrdine nel business layer.", ex);
            }
        }

        public async Task<Prodotto?> RecuperaProdottoAsync(int idProdotto)
        {
            try
            {
                return await oDL.RecuperaProdottoAsync(idProdotto);
            }

            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'entità Prodotto nel business layer.", ex);
            }
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

        //----------------------------------------------DAMIANO---------------------------------------------------


        public async Task<ActionResult> DeleteOrdineAsync(int idUtente, int idDettaglioOrdine)
        {
            try
            {
                // Chiamata al business layer per verificare l'esistenza dell'ordine
                int? idOrdineEsistente = await oDL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);

                if (idOrdineEsistente == null)
                {
                    return ErrorContentResult("l'ordine non esiste", 400);
                }

                // Altri controlli di business, se necessario...
                //Chiamata al business layer per recuperare lo stato dell'ordine

                int statoOrdine = (int)await oDL.RecuperaStatoOrdineAsync((int)idOrdineEsistente);
                if (statoOrdine == 3)
                {
                    return ErrorContentResult("l'ordine è chiuso", 400);

                }

                // Chiamata al business layer per recuperare la quantità del prodotto

                int? idProdotto = await oDL.RecuperaIdProdottoAsync((int)idOrdineEsistente);
                if (idProdotto == null)
                {
                    return ErrorContentResult("il prodotto non esiste", 404);
                }

                    int? quantitaProdottoDisponibile = await oDL.RecuperaQuantitaProdottoAsync((int)idProdotto);

                // Chiamata al business layer per modificare l'ordine (transazioni)
                bool successo = await oDL.DeleteOrdineAsync((int)idOrdineEsistente);

                if (successo)
                {
                    return ErrorContentResult (204); // Operazione completata con successo
                }
                else
                {
                    return ErrorContentResult("Errore durante la cancellazione dell'ordine", 400);
                    // Errore nell'operazione di cancellazione dell'ordine
                }
            }
            catch (Exception)
            {
                // Gestione degli errori
                return ErrorContentResult("Si è verificato un errore durante la cancellazione dell'ordine", 400);

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

                return await oDL.AddUtenteAsync(utente);
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

        private ContentResult ErrorContentResult(int statusCode = 400)
        {
            return new ContentResult
            {
                Content = string.Empty,
                ContentType = "text/plain",
                StatusCode = statusCode
            };
        }

        //Adriano
        public async Task<ActionResult<int>> nuovoOrdine(int idUtente, int idProdotto, int quantità)
        {
            Prodotto prodotto;
            if (oDL.prodottoExists(idProdotto))
            {
                prodotto = await oDL.getProdottoAsync(idProdotto);
            }
            else// codice errore 404
            {
               return ErrorContentResult("Client Error. \nProdotto non presente nel database.", 404);
            }

            if (prodotto != null && quantità != 0
                && prodotto.Quantità >= quantità)
            {
                prodotto.Quantità -= quantità;
                try 
                { 
                    int idOrdine = await oDL.nuovoOrdine(idUtente, prodotto, quantità);
                    return idOrdine;// codice 201
                }
                catch (TransactionAbortedException)// codice errore 500
                {
                    return ErrorContentResult("Server Error.\nSi è verificato un errore durante l'inserimento dell'ordine", 500);
                }
                catch (TransactionException)// codice errore 500
                {
                    return ErrorContentResult("Server Error.\nSi è verificato un errore durante l'aggiornamento del database", 500);
                }
                catch (Exception)// codice errore 400
                {
                    return ErrorContentResult( "Generic Error", 400);
                }
            }
            else// codice errore 400
            {
                return ErrorContentResult("Client Error. \nLa reperibilità del prodotto è minore della richiesta effettuata.", 400);
            }
        }

        // Leonardo
        public (bool isSuccess, string message) LoginUser(string email, string password)
        {
            try
            {
                return oDL.LoginUser(email, password);
            }

            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero di email e password dell'utente nel Business Layer.", ex);
            }
        }
    }
    }

