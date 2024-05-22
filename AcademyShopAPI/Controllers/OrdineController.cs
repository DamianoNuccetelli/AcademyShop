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
    public class OrdineController : ControllerBase
    {
        private readonly AcademyShopDBContext _context;

        public OrdineController(AcademyShopDBContext context)
        {
            _context = context;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdine(int id, Ordine ordine)
        {
            if (id != ordine.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdineExists(id))
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
