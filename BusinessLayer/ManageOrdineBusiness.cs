using AcademyShopAPI.Models;
using DataLayer;
using DataLayer.Repository;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLayer
{
    public class ManageOrdineBusiness
    {
        //private readonly IRepositoryWithDtoAsync<Ordine, OrdineModificatoDTO> _ordineModificatoDto; Damiano
        //private readonly IPutOrderRepository _orderPutRepository; Damiano
        private readonly IRepositoryWithDtoAsync<Ordine, OrdineDettaglioDTOperGET> _ordineDtoPerGet; //Gabriele e Renato
        private readonly ManageOrdineData oODL;
        private readonly ManageUtenteData oUDL;

        public ManageOrdineBusiness(IRepositoryWithDtoAsync<Ordine, OrdineDettaglioDTOperGET> ordineDtoPerGet, ManageOrdineData manageOrdineData, ManageUtenteData _oUDL)
        { 
            _ordineDtoPerGet = ordineDtoPerGet;
            oODL = manageOrdineData;
            oUDL = _oUDL;
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
                return await oODL.UtenteExists(id);
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
                bool utenteExists = await oUDL.CheckUtenteExistsById(userId);

                if (!utenteExists)
                {
                    // Se l'utente non esiste, restituisci una risposta di errore 400 (Bad Request)
                    throw new ArgumentException();
                }

                // Chiama il metodo corrispondente del data layer per recuperare gli ordini dell'utente
                var ordini = await oODL.GetOrdiniByUserId(userId);

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
            var idOrdineEsistente = await oODL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);
            if (idOrdineEsistente == null)
            {
                return (false, "L'ordine non esiste.", 404, null);
            }

            var statoOrdine = await oODL.RecuperaStatoOrdineAsync((int)idOrdineEsistente);
            if (statoOrdine == 3)
            {
                return (false, "L'ordine è chiuso.", 400, null);
            }

            var idProdotto = await oODL.RecuperaIdProdottoAsync((int)idOrdineEsistente);
            if (idProdotto == null)
            {
                return (false, "Il prodotto non esiste.", 404, null);
            }

            var quantitaProdottoDisponibile = await oODL.RecuperaQuantitaProdottoAsync((int)idProdotto);
            if (quantitaProdottoDisponibile <= quantita || quantitaProdottoDisponibile == 0)
            {
                return (false, "La quantità disponibile non è sufficiente.", 400, null);
            }

            // entità
            var ordine = await oODL.RecuperaOrdineAsync((int)idOrdineEsistente);
            var dettaglioOrdine = await oODL.RecuperaDettaglioOrdineAsync((int)idDettaglioOrdine);
            var prodotto = await oODL.RecuperaProdottoAsync((int)idProdotto);

            if (ordine == null || dettaglioOrdine == null || prodotto == null)
            {
                return (false, "Errore nel recupero dei dati.", 500, null);
            }


            // esecuzione transazione data layer
            var success = await oODL.ModificaOrdineTransazioneAsync(ordine, dettaglioOrdine, prodotto, (int)statoOrdine, quantita);
            if (!success)
            {
                return (false, "Errore nell'operazione di modifica dell'ordine.", 500, null);
            }

            var ordineModificato = await oODL.RecuperaOrdineModificatoAsync((int)idOrdineEsistente);
            var ordineModificatoDTO = MapToDTO(ordineModificato);

            return (true, string.Empty, 200, ordineModificatoDTO);
        }

        public async Task<Ordine?> RecuperaOrdineAsync(int idOrdineEsistente)
        {
            try
            {
                return await oODL.RecuperaOrdineAsync(idOrdineEsistente);
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
                return await oODL.RecuperaDettaglioOrdineAsync(idDettaglioOrdine);
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
                return await oODL.RecuperaProdottoAsync(idProdotto);
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
                return await oODL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);
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
                return await oODL.RecuperaStatoOrdineAsync(idOrdineEsistente);
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
                return await oODL.RecuperaIdProdottoAsync(idOrdineEsistente);
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
                return await oODL.RecuperaQuantitaProdottoAsync(idOrdineEsistente);
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
                int? idOrdineEsistente = await oODL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);

                if (idOrdineEsistente == null)
                {
                    return ErrorContentResult("l'ordine non esiste", 400);
                }

                // Altri controlli di business, se necessario...
                //Chiamata al business layer per recuperare lo stato dell'ordine

                int statoOrdine = (int)await oODL.RecuperaStatoOrdineAsync((int)idOrdineEsistente);
                if (statoOrdine == 3)
                {
                    return ErrorContentResult("l'ordine è chiuso", 400);

                }

                // Chiamata al business layer per recuperare la quantità del prodotto

                int? idProdotto = await oODL.RecuperaIdProdottoAsync((int)idOrdineEsistente);
                if (idProdotto == null)
                {
                    return ErrorContentResult("il prodotto non esiste", 404);
                }

                int? quantitaProdottoDisponibile = await oODL.RecuperaQuantitaProdottoAsync((int)idProdotto);

                // Chiamata al business layer per modificare l'ordine (transazioni)
                bool successo = await oODL.DeleteOrdineAsync((int)idOrdineEsistente);

                if (successo)
                {
                    return ErrorContentResult("", 204); // Operazione completata con successo
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

        private ContentResult ErrorContentResult(string errorMessage, int statusCode = 400)
        {
            return new ContentResult
            {
                Content = errorMessage,
                ContentType = "text/plain",
                StatusCode = statusCode
            };
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
                if (oODL.DettaglioOrdineExists(dettaglioOrdineId) == false)
                {
                    throw new ArgumentException("Dettaglio ordine non trovato.");
                }

                bool utenteExists = await oUDL.CheckUtenteExistsById(userId);

                if (utenteExists == false)
                {
                    throw new ArgumentException("Utente non trovato");
                }
                return await oODL.GetOrdineDettaglioAsync(userId, dettaglioOrdineId);
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nell'esecuzione del programma: ----> " + ex.Message);
            }
        }
        public async Task<ActionResult<int>> nuovoOrdine(int idUtente, int idProdotto, int quantità)
        {
            Prodotto prodotto; if (oODL.prodottoExists(idProdotto)) { prodotto = await oODL.getProdottoAsync(idProdotto); }
            else// codice errore 404
            { return ErrorContentResult("Client Error. \nProdotto non presente nel database.", 404); }
            if (prodotto != null && quantità != 0 && prodotto.Quantità >= quantità)
            {
                prodotto.Quantità -= quantità; try
                {
                    int idOrdine = await oODL.nuovoOrdine(idUtente, prodotto, quantità); return idOrdine;// codice 201
                }
                catch (TransactionAbortedException)// codice errore 500
                { return ErrorContentResult("Server Error.\nSi è verificato un errore durante l'inserimento dell'ordine", 500); }
                catch (TransactionException)// codice errore 500
                {
                    return ErrorContentResult("Server Error.\nSi è verificato un errore durante l'aggiornamento del database", 500);
                }
                catch (Exception)// codice errore 400
                {
                    return ErrorContentResult("Generic Error", 400);
                }
            }
            else// codice errore 400
            {
                return ErrorContentResult("Client Error. \nLa reperibilità del prodotto è minore della richiesta effettuata.", 400);
            }
        }


    }
}
