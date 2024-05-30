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
        private readonly ManageOrdineBusiness oOBL;
        private readonly ManageUtenteBusiness oUBL;

        public OrdineController(ManageOrdineBusiness _oOBL, ManageUtenteBusiness _oUBL)
        {
            oOBL = _oOBL;
            oUBL = _oUBL;
        }
        //Florea Renato Chiamata al BusinessLayer 
        [HttpGet("GetAllOrdiniByUserId/{userId}")]
        public async Task<IActionResult> GetOrdiniByUserId(int userId)
        {
            try
            {
                var ordini = await oOBL.GetOrdiniByUserId(userId);
               
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
            var (success, message, statusCode, ordineModificato) = await oOBL.ModificaOrdineCompletaAsync(idUtente, idDettaglioOrdine, quantita);

            return success ? StatusCode(200, ordineModificato) : StatusCode(statusCode, message);

        }


        // DELETE: api/Ordine/5
        [HttpDelete]
        public async Task<IActionResult> DeleteOrdine(int idUtente, int idDettaglioOrdine)
        {

                var deleteOrder = await oOBL.DeleteOrdineAsync(idUtente, idDettaglioOrdine);
                return deleteOrder;
            
        }

        [HttpPost("AddOrdine")]
        /* INSERIMENTO NUOVO ORDINE*/
        public async Task<ActionResult<int>> addOrdineAsync(int idUtente, int idProdotto, int quantità)
        {
            var result = await  oOBL.addOrdine(idUtente, idProdotto, quantità);
            return result.Value >0 ? StatusCode(201, new { id = result.Value }) : result;
        }


        [HttpGet("GetOrdineByUser&Dettaglio{userId}/{dettaglioOrdineId}")]
        public async Task<ActionResult> GetOrdineDettaglio(int userId, int dettaglioOrdineId)
        {
            try
            {
                var result = await oOBL.GetOrdineDettaglioAsync(userId, dettaglioOrdineId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Si è verificato un errore durante il recupero dell'ordine: ---> " + ex.Message);
            }

        }


    }
}
