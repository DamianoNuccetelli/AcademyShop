using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademyShopAPI.Models;

namespace AcademyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottoController : ControllerBase
    {
        private readonly BusinessLayer.ManageBusiness _oBL;

        public ProdottoController(BusinessLayer.ManageBusiness oBL)
        {
            _oBL = oBL;
        }
   

        //NON CANCELLARE I DUE METODI COMMENTATI - GABRIELE 2021-06-15
        [HttpGet("GetOrdineDettaglio/{userId}/{dettaglioOrdineId}")]
        public async Task<ActionResult> GetOrdineDettaglio(int userId, int dettaglioOrdineId)
        {
            try
            {
                var result = await _oBL.GetOrdineDettaglioAsync(userId, dettaglioOrdineId);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpGet("GetOrdineDettaglio")]
        //public async Task<ActionResult> GetOrdineDettaglio(int userId, int dettaglioOrdineId)
        //{
        //    try
        //    {
        //        var result = await (from ordine in _context.Ordines
        //                            join statoOrdine in _context.StatoOrdines on ordine.FkIdStato equals statoOrdine.Id
        //                            join dettaglioOrdine in _context.DettaglioOrdines on ordine.Id equals dettaglioOrdine.FkIdOrdine
        //                            join prodotto in _context.Prodottos on dettaglioOrdine.FkIdProdotto equals prodotto.Id
        //                            where ordine.FkIdUtente == userId
        //                                  && dettaglioOrdine.Id == dettaglioOrdineId
        //                            select new
        //                            {
        //                                ProdottoNome = prodotto.Nome,
        //                                ProdottoDescrizione = prodotto.Descrizione,
        //                                StatoOrdineDescrizione = statoOrdine.Descrizione,
        //                                dettaglioOrdine.Quantita,
        //                                prodotto.Id,
        //                                ordine.DataRegistrazione,
        //                                ordine.DataAggiornamento
        //                            }).FirstOrDefaultAsync();

        //        if (result == null)
        //        {
        //            return StatusCode(400, "Errore nell'esecuzione della query");
        //        }

        //        return Ok(result); 
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message); 
        //    }
        //}


        //[HttpGet("GetOrdineDettaglio/{userId}")]
        //public async Task<ActionResult> GetOrdineDettaglio(int userId, [FromQuery] int dettaglioOrdineId)
        //{
        //    try
        //    {
        //        var result = await (from ordine in _context.Ordines
        //                            join statoOrdine in _context.StatoOrdines on ordine.FkIdStato equals statoOrdine.Id
        //                            join dettaglioOrdine in _context.DettaglioOrdines on ordine.Id equals dettaglioOrdine.FkIdOrdine
        //                            join prodotto in _context.Prodottos on dettaglioOrdine.FkIdProdotto equals prodotto.Id
        //                            where ordine.FkIdUtente == userId
        //                                  && dettaglioOrdine.Id == dettaglioOrdineId
        //                            select new
        //                            {
        //                                ProdottoNome = prodotto.Nome,
        //                                ProdottoDescrizione = prodotto.Descrizione,
        //                                StatoOrdineDescrizione = statoOrdine.Descrizione,
        //                                dettaglioOrdine.Quantita,
        //                                prodotto.Id,
        //                                ordine.DataRegistrazione,
        //                                ordine.DataAggiornamento
        //                            }).FirstOrDefaultAsync();

        //        if (result == null)
        //        {
        //            return StatusCode(400, "Errore nell'esecuzione della query");
        //        }

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}



    }
}
