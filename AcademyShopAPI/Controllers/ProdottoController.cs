using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcademyShopAPI.Models;
using BusinessLayer;

namespace AcademyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottoController : ControllerBase
    {
        private readonly ManageProdottoBusiness _prodottoBusiness;

        public ProdottoController(ManageProdottoBusiness prodottoBusiness)
        {
            _prodottoBusiness = prodottoBusiness;
        }

        // GET: api/Prodotto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prodotto>>> GetProdottos()
        {
            var prodottos = await _prodottoBusiness.GetProdottosAsync();
            return Ok(prodottos);
        }

        // GET: api/Prodotto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prodotto>> GetProdotto(int id)
        {
            var prodotto = await _prodottoBusiness.GetProdottoAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return Ok(prodotto);
        }

        // POST: api/Prodotto
        [HttpPost]
        public async Task<ActionResult<Prodotto>> AddProdotto(Prodotto prodotto)
        {
            var newProdotto = await _prodottoBusiness.AddProdottoAsync(prodotto);
            return CreatedAtAction(nameof(GetProdotto), new { id = newProdotto.Id }, newProdotto);
        }

        // PUT: api/Prodotto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProdotto(int id, Prodotto prodotto)
        {
            if (id != prodotto.Id)
            {
                return BadRequest();
            }

            var updateResult = await _prodottoBusiness.UpdateProdottoAsync(prodotto);
            if (!updateResult)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Prodotto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdotto(int id)
        {
            var deleteResult = await _prodottoBusiness.DeleteProdottoAsync(id);
            if (!deleteResult)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Extra: Check if a product exists
        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> ProdottoExists(int id)
        {
            var exists = await _prodottoBusiness.ProdottoExistsAsync(id);
            return Ok(exists);
        }
    }
}
