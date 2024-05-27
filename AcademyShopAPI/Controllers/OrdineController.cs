using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademyShopAPI.Models;
using BusinessLayer;

namespace AcademyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdineController : ControllerBase
    {
        private readonly AcademyShopDBContext _context;
        private readonly ManageBusiness oBL;

        public OrdineController(ManageBusiness _oBL)
        {
           oBL = _oBL;
        }
        //Florea Renato Chiamata al BusinessLayer 
        [HttpGet("GetOrdiniByUserId/{userId}")]
        public async Task<IActionResult> GetOrdiniByUserId(int userId)
        {

            try
            {
                int? utentePresente = await oBL.UtenteExists(userId);

                if(utentePresente == null)
                {
                    return BadRequest("L'utente non esiste");
                }

                var ordini = await oBL.GetOrdiniByUserId(userId);
                if (ordini.Count > 0)
                {
                    // Se ci sono ordini, restituisci gli ordini dell'utente
                    return Ok( ordini);
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
       
        // GET: api/Ordine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ordine>>> GetOrdines()
        {
            return await _context.Ordines.ToListAsync();
        }

        // GET: api/Ordine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ordine>> GetOrdine(int id)
        {
            var ordine = await _context.Ordines.FindAsync(id);

            if (ordine == null)
            {
                return NotFound();
            }

            return ordine;
        }

        // PUT: api/Ordine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutOrdine(int idUtente, int idDettaglioOrdine, int quantita)
        {
            
            try
            {
                // Verifica dell'utente loggato
                //int idUtenteLoggato = (int)await oBL.RecuperaIdUtente(password, email);

                // Chiamata al business layer per verificare l'esistenza dell'ordine
                int? idOrdineEsistente = await oBL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);

                if (idOrdineEsistente == null)
                {
                    return BadRequest("L'ordine non esiste.");
                }

                // Altri controlli di business, se necessario...
                //Chiamata al business layer per recuperare lo stato dell'ordine

                int statoOrdine = (int)await oBL.RecuperaStatoOrdineAsync((int)idOrdineEsistente);
                if (statoOrdine == 3)
                {
                    return BadRequest("L'ordine è chiuso"); // Stato dell'ordine chiuso
                }

                // Chiamata al business layer per recuperare la quantità del prodotto

                int? quantitaProdottoDisponibile = await oBL.RecuperaQuantitaProdottoAsync((int)idOrdineEsistente);

                if (quantitaProdottoDisponibile <= quantita || quantitaProdottoDisponibile == 0)
                {
                    return BadRequest("La quantita disponibile non è sufficiente"); // Quantità disponibile non sufficiente
                }

                int? idProdotto = await oBL.RecuperaIdProdottoAsync((int)idOrdineEsistente);

                if (idProdotto == null)
                {
                    return BadRequest("Il prodotto non esiste"); // Prodotto non esistente
                }

                // Chiamata al business layer per modificare l'ordine (transazioni)
                bool successo = await oBL.ModificaOrdineAsync((int)idOrdineEsistente, (int)idProdotto, quantita);

                if (successo)
                {
                    return NoContent(); // Operazione completata con successo
                }
                else
                {
                    return BadRequest(); // Errore nell'operazione di modifica dell'ordine
                }
            }
            catch (Exception ex)
            {
                // Gestione degli errori
                return StatusCode(500, "Si è verificato un errore durante la modifica dell'ordine.");
            }
        }


        // POST: api/Ordine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ordine>> PostOrdine(Ordine ordine)
        {
            _context.Ordines.Add(ordine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdine", new { id = ordine.Id }, ordine);
        }

        // DELETE: api/Ordine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdine(int id)
        {
            var ordine = await _context.Ordines.FindAsync(id);
            if (ordine == null)
            {
                return NotFound();
            }

            _context.Ordines.Remove(ordine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdineExists(int id)
        {
            return _context.Ordines.Any(e => e.Id == id);
        }
    }
}
