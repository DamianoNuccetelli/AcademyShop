using System;
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
                var ordini = await oBL.GetOrdiniByUserId(userId);
               
                    return Ok(ordini);  
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
        public async Task<ActionResult<int>> nuovoOrdineAsync(int idUtente, int idProdotto, int quantità)
        {
            var result = await  oBL.nuovoOrdine(idUtente, idProdotto, quantità);
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
