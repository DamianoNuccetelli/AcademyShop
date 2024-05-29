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
        private readonly ManageUtenteData oUDL;


        public ManageBusiness(ManageData _oDL, ManageUtenteData oUDL)
        {
            oDL = _oDL;
            this.oUDL = oUDL;
        }


        
        //Daniel -> Aggiunta e rimozione dell'utente dal db
        public async Task<IEnumerable<Utente>> GetUtentes()
        {
            return await oUDL.GetUtentes();
        }

        public async Task<Utente> GetUtente(int id)
        {
            return await oUDL.GetUtente(id);
        }

        //public async Task<ActionResult<Utente>> PostUtente(Utente utente)
        //{
        //    try
        //    {
        //        // Verifico se un utente è gia esistente
        //        if (await oUDL.CheckUtenteExists(utente))
        //        {
        //            return ErrorContentResult("Un utente con lo stesso codice fiscale o email esiste già.", 409);
        //        }

        //        // Controllo tramite regex del codice fiscale
        //        if (!IsValidCodiceFiscale(utente.CodiceFiscale))
        //        {
        //            return ErrorContentResult("Un utente con lo stesso codice fiscale o email esiste già.");
        //        }

        //        // Controllo tramite regex dell'email
        //        if (!IsValidEmail(utente.Email))
        //        {
        //            return ErrorContentResult("L'email non è valida.");
        //        }

        //        // Controllo sulla data di nascita (l'utente deve essere nato tra il 1900 e il giorno d'oggi)
        //        if (!IsValidBirthDate(utente.DataNascita))
        //        {
        //            return ErrorContentResult("La data di nascita non è valida.");
        //        }


        //        if (utente.CodiceFiscale.Length != 16 || utente.Password.Length != 16 || utente.ProvinciaNascita.Length != 2 || (utente.Sesso != "M" && utente.Sesso != "F"))
        //        {
        //            return ErrorContentResult("Uno o più campi non rispettano la lunghezza o i valori richiesti.");
        //        }

        //        return await oUDL.AddUtenteAsync(utente);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Errore durante la creazione dell'utente: {ex.Message}");
        //    }
        //}

        //public async Task<ActionResult<Utente>> DeleteUtente(int id)
        //{
        //    try
        //    {
        //        var utente = await oUDL.GetUtente(id);
        //        if (utente == null)
        //        {
        //            return new NotFoundResult();
        //        }

        //        var result = await oUDL.DeleteUtente(id);

        //        //return Ok($"Utente '{utente.Nome} {utente.Cognome}' eliminato con successo.");
        //        return new OkObjectResult($"Utente '{utente.Nome} {utente.Cognome}' eliminato con successo.");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Errore durante l'eliminazione dell'utente con ID {id} nel livello della logica di business.", ex);
        //    }
        //}

        //private bool IsValidCodiceFiscale(string codiceFiscale)
        //{
        //    string regexPattern = @"^[A-Z]{6}\d{2}[ABCDEHLMPRST]\d{2}[A-Z]\d{3}[A-Z]$";
        //    return System.Text.RegularExpressions.Regex.IsMatch(codiceFiscale, regexPattern);
        //}

        //private bool IsValidEmail(string email)
        //{
        //    string regexPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        //    return System.Text.RegularExpressions.Regex.IsMatch(email, regexPattern);
        //}

        //private bool IsValidBirthDate(DateOnly birthDate)
        //{
        //    DateOnly minDate = new DateOnly(1900, 1, 1);
        //    DateOnly maxDate = DateOnly.FromDateTime(DateTime.UtcNow);
        //    return birthDate >= minDate && birthDate <= maxDate;
        //}

        //private ContentResult ErrorContentResult(string errorMessage, int statusCode = 400)
        //{
        //    return new ContentResult
        //    {
        //        Content = errorMessage,
        //        ContentType = "text/plain",
        //        StatusCode = statusCode
        //    };
        //}

        //private ContentResult ErrorContentResult(int statusCode = 400)
        //{
        //    return new ContentResult
        //    {
        //        Content = string.Empty,
        //        ContentType = "text/plain",
        //        StatusCode = statusCode
        //    };
        //}

        ////Adriano
        //public async Task<ActionResult<int>> nuovoOrdine(int idUtente, int idProdotto, int quantità)
        //{
        //    Prodotto prodotto;
        //    if (.prodottoExists(idProdotto))
        //    {
        //        prodotto = await oDL.getProdottoAsync(idProdotto);
        //    }
        //    else// codice errore 404
        //    {
        //       return ErrorContentResult("Client Error. \nProdotto non presente nel database.", 404);
        //    }

        //    if (prodotto != null && quantità != 0
        //        && prodotto.Quantità >= quantità)
        //    {
        //        prodotto.Quantità -= quantità;
        //        try 
        //        { 
        //            int idOrdine = await oDL.nuovoOrdine(idUtente, prodotto, quantità);
        //            return idOrdine;// codice 201
        //        }
        //        catch (TransactionAbortedException)// codice errore 500
        //        {
        //            return ErrorContentResult("Server Error.\nSi è verificato un errore durante l'inserimento dell'ordine", 500);
        //        }
        //        catch (TransactionException)// codice errore 500
        //        {
        //            return ErrorContentResult("Server Error.\nSi è verificato un errore durante l'aggiornamento del database", 500);
        //        }
        //        catch (Exception)// codice errore 400
        //        {
        //            return ErrorContentResult( "Generic Error", 400);
        //        }
        //    }
        //    else// codice errore 400
        //    {
        //        return ErrorContentResult("Client Error. \nLa reperibilità del prodotto è minore della richiesta effettuata.", 400);
        //    }
        //}

        //// Leonardo
        //public async Task<ActionResult<int>> LoginUser(string email, string password)
        //{
        //    try
        //    {   Utente utente = await oDL.LoginUser(email, password);
                
        //        if (utente != null)
        //        {
        //            return utente.Id;
        //        }
        //        else
        //        {
        //            return ErrorContentResult("Client Error. \nEmail o Password non corretta.", 404);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //       return ErrorContentResult("Server Error.\nSi è verificato un errore durante l'accesso del database", 500);
        //    }
        //}
    }
    }

