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
            catch (Exception)
            {
                // Gestione degli errori
                return StatusCode(500, "Si è verificato un errore durante la modifica dell'ordine.");
            }
        }


        // DELETE: api/Ordine/5
        [HttpDelete]
        public async Task<IActionResult> DeleteOrdine(int idUtente, int idDettaglioOrdine)
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

                int? idProdotto = await oBL.RecuperaIdProdottoAsync((int)idOrdineEsistente);

                if (idProdotto == null)
                {
                    return BadRequest("Il prodotto non esiste"); // Prodotto non esistente
                }

                // Chiamata al business layer per modificare l'ordine (transazioni)
                bool successo = await oBL.DeleteOrdineAsync((int)idOrdineEsistente);

                if (successo)
                {
                    return NoContent(); // Operazione completata con successo
                }
                else
                {
                    return BadRequest(); // Errore nell'operazione di cancellazione dell'ordine
                }
            }
            catch (Exception)
            {
                // Gestione degli errori
                return StatusCode(500, "Si è verificato un errore durante la modifica dell'ordine.");
            }
        }

        private bool OrdineExists(int id)
        {
            return _context.Ordines.Any(e => e.Id == id);
        }

        [HttpPost]
        /* INSERIMENTO NUOVO ORDINE*/
        public async Task<ActionResult<int>> NuovoOrdineAsync(int idUtente, int idprodotto, int quantità)
        {
            // OrdineBusiness _business = new OrdineBusiness();
            int idOrdine;

            try
            {
                idOrdine = await oBL.NuovoOrdine(idUtente, idprodotto, quantità);
                return StatusCode(201, idOrdine);
            }
            catch (ArgumentException)
            {
                return StatusCode(400, "Client Error. \nLa reperibilità del prodotto è minore della richiesta effettuata.");
            }
            catch (TransactionAbortedException)
            {
                return StatusCode(500, "Server Error.\nSi è verificato un errore durante l'inserimento dell'ordine");
            }
            catch (TransactionException)
            {
                return StatusCode(500, "Server Error.\nSi è verificato un errore durante l'aggiornamento del database");
            }
            catch (Exception)
            {
                return StatusCode(400, "Generic Error");
            }


        }
    }
}
