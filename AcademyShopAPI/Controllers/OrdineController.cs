﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademyShopAPI.Models;
using BusinessLayer;
using System.Transactions;
using Humanizer.Localisation;

namespace AcademyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdineController : ControllerBase
    {
        private readonly ManageBusiness oBL;

        public OrdineController(ManageBusiness _oBL)
        {
            oBL = _oBL;
        }
        //Florea Renato Chiamata al BusinessLayer 

        [HttpGet("GetAllOrdiniByUserId/{userId}")]
        public async Task<IActionResult> GetOrdiniByUserId(int userId)
        {

            try
            {
                // Verifica se l'utente esiste
                int? utentePresente = await oBL.UtenteExists(userId);

                if (utentePresente == null)
                {
                    // Se l'utente non esiste, restituisce una risposta di errore 400 (Bad Request)
                    return BadRequest("L'utente non esiste");
                }
                // Ottiene gli ordini dell'utente ereditandoli dal ManageBusiness
                var ordini = await oBL.GetOrdiniByUserId(userId);
                //Se la lista ordini contiene almeno un elemento, restituisce una risposta  (OK) con la lista degli ordini.
                if (ordini.Count > 0)
                {
                    // Se ci sono ordini, restituisci gli ordini dell'utente
                    return Ok(ordini);
                }
                else
                {
                    // Se non ci sono ordini, restituisci un messaggio appropriato
                    return StatusCode(400, "Non ci sono ordini per questo utente.");
                }
            }


            catch (Exception ex)
            {
                // Gestisci eventuali errori qui 
                return StatusCode(500, "Si è verificato un errore durante il recupero degli ordini: " + ex.Message);
            }
        }

        // PUT: api/Ordine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idUtente}/{idDettaglioOrdine}/{quantita}")]
        public async Task<IActionResult> PutOrdine(int idUtente, int idDettaglioOrdine, int quantita)
        {
            var (success, message, statusCode, ordineModificato) = await oBL.ModificaOrdineCompletaAsync(idUtente, idDettaglioOrdine, quantita);

            if (success)
            {
                return Ok(ordineModificato);
            }

            return StatusCode(statusCode, message);
        }




        // DELETE: api/Ordine/5
        [HttpDelete]
        public async Task<IActionResult> DeleteOrdine(int idUtente, int idDettaglioOrdine)
        {

                var deleteOrder = await oBL.DeleteOrdineAsync(idUtente, idDettaglioOrdine);
                return deleteOrder;
            
        }

        [HttpPost]
        /* INSERIMENTO NUOVO ORDINE*/
        public async Task<ActionResult<int>> nuovoOrdineAsync(int idUtente, int idprodotto, int quantità)
        {
            var result = await  oBL.nuovoOrdine(idUtente, idprodotto, quantità);
            return result.Value >0 ? StatusCode(201, new { id = result.Value }) : result;
        }


        [HttpGet("GetOrdineByUser&Dettaglio{userId}/{dettaglioOrdineId}")]
        public async Task<ActionResult> GetOrdineDettaglio(int userId, int dettaglioOrdineId)
        {
            try
            {
                var result = await oBL.GetOrdineDettaglioAsync(userId, dettaglioOrdineId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Si è verificato un errore durante il recupero dell'ordine: ---> " + ex.Message);
            }

        }


    }
}
