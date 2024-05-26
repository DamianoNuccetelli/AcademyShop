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
        private readonly AcademyShopDBContext _context;

        public ProdottoController(AcademyShopDBContext context)
        {
            _context = context;
        }

        // GET: api/Prodotto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prodotto>>> GetProdottos()
        {
            return await _context.Prodottos.ToListAsync();
        }

        // GET: api/Prodotto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prodotto>> GetProdotto(int id)
        {
            var prodotto = await _context.Prodottos.FindAsync(id);

            if (prodotto == null)
            {
                return NotFound();
            }

            return prodotto;
        }

        // PUT: api/Prodotto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdotto(int id, Prodotto prodotto)
        {
            if (id != prodotto.Id)
            {
                return BadRequest();
            }

            _context.Entry(prodotto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdottoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Prodotto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prodotto>> PostProdotto(Prodotto prodotto)
        {
            _context.Prodottos.Add(prodotto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdotto", new { id = prodotto.Id }, prodotto);
        }

        // DELETE: api/Prodotto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdotto(int id)
        {
            var prodotto = await _context.Prodottos.FindAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }

            _context.Prodottos.Remove(prodotto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdottoExists(int id)
        {
            return _context.Prodottos.Any(e => e.Id == id);
        }


        //NON CANCELLARE I DUE METODI COMMENTATI - GABRIELE 2021-06-15
        [HttpGet("GetOrdineDettaglio/{userId}/{dettaglioOrdineId}")]
        public async Task<ActionResult> GetOrdineDettaglio(int userId, int dettaglioOrdineId)
        {
            try
            {
                var result = await (from ordine in _context.Ordines
                                    join statoOrdine in _context.StatoOrdines on ordine.FkIdStato equals statoOrdine.Id
                                    join dettaglioOrdine in _context.DettaglioOrdines on ordine.Id equals dettaglioOrdine.FkIdOrdine
                                    join prodotto in _context.Prodottos on dettaglioOrdine.FkIdProdotto equals prodotto.Id
                                    where ordine.FkIdUtente == userId
                                          && dettaglioOrdine.Id == dettaglioOrdineId
                                    select new
                                    {
                                        ProdottoNome = prodotto.Nome,
                                        ProdottoDescrizione = prodotto.Descrizione,
                                        StatoOrdineDescrizione = statoOrdine.Descrizione,
                                        dettaglioOrdine.Quantita,
                                        prodotto.Id,
                                        ordine.DataRegistrazione,
                                        ordine.DataAggiornamento
                                    }).FirstOrDefaultAsync();

                if (result == null)
                {
                    return StatusCode(400, "Errore nell'esecuzione della query");
                }

                return Ok(result);
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
