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
        private readonly ManageOrdineBusiness oOBL;

        public OrdineController(ManageOrdineBusiness _oOBL)
        {
            oOBL = _oOBL;
        }
        //Gabriele
        [HttpGet("GetOrdineByUser&Dettaglio{userId}")]
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
        //Florea Renato 
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
        //Damiano
        [HttpPut("{idUtente}/{idDettaglioOrdine}/{quantita}")]
        public async Task<IActionResult> PutOrdine(int idUtente, int idDettaglioOrdine, int quantita)
        {
            var (success, message, statusCode, ordineModificato) = await oOBL.ModificaOrdineCompletaAsync(idUtente, idDettaglioOrdine, quantita);
            return success ? StatusCode(200, ordineModificato) : StatusCode(statusCode, message);
        }
        //Adriano
        [HttpPost("Add-Ordine")]
        public async Task<ActionResult<int>> addOrdineAsync(int idUtente, int idProdotto, int quantità)
        {
            var result = await  oOBL.addOrdine(idUtente, idProdotto, quantità);
            return result.Value >0 ? StatusCode(201, new { id = result.Value }) : result;
        }
        //Francesco
        [HttpDelete("Delete-Ordine")]
        public async Task<IActionResult> DeleteOrdine(int idUtente, int idDettaglioOrdine)
        {
            var deleteOrder = await oOBL.DeleteOrdineAsync(idUtente, idDettaglioOrdine);
            return deleteOrder;
        }
    }
}
