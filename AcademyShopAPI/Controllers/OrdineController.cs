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
        //Florea Renato Chiamata al BusinessLayer 
        
        [HttpGet("GetAllOrdiniByUserId/{userId}")]
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
        [HttpPut("{idUtente}/{idDettaglioOrdine}/{quantita}")]
        public async Task<IActionResult> PutOrdine(int idUtente, int idDettaglioOrdine, int quantita)
        {
            try
            {
                var result = await oBL.ModificaOrdineCompletaAsync(idUtente, idDettaglioOrdine, quantita);

                if (result.success)
                {
                    return Ok(result.ordineModificato); // Restituisce il DTO dell'ordine modificato
                }
                else
                {
                    return StatusCode(result.statusCode, result.message); // Gestisce l'errore con un codice di stato appropriato
                }
            }
            catch (Exception ex)
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
                var UtenteEsistente = await oBL.GetUtente(idUtente);
                if (UtenteEsistente == null)
                {
                    return BadRequest("L'utente non esiste.");
                }
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
            try
            {
                int idOrdine = await oBL.NuovoOrdine(idUtente, idprodotto, quantità);
                return StatusCode(201, idOrdine);
            }
            catch (ArgumentException)
            {
                return StatusCode(400, "Client Error. \nLa reperibilità del prodotto è minore della richiesta effettuata.");
            }
            catch (KeyNotFoundException)
            {
                return StatusCode(404, "Client Error. \nProdotto non presente nel database.");
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



        //NON CANCELLARE I DUE METODI COMMENTATI - GABRIELE 2021-06-15
        [HttpGet("GetOrdineByUser{userId}/{dettaglioOrdineId}")]
        public async Task<ActionResult> GetOrdineDettaglio(int userId, int dettaglioOrdineId)
        {
            try
            {
                var result = await oBL.GetOrdineDettaglioAsync(userId, dettaglioOrdineId);

                if (result == null)
                {
                    return StatusCode(400, "Errore, non sono presenti combinazioni di utenti/prodotti");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Errore nella richiesta dei dati\n" + ex.Message);
            }
        }


    }
}
