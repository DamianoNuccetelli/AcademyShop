using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcademyShopAPI.Models;
using BusinessLayer;
using DtoLayer.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottoController : ControllerBase
    {
        private readonly ManageProdottoBusiness _prodottoBusiness;
        private readonly IMapper _mapper;

        public ProdottoController(ManageProdottoBusiness prodottoBusiness, IMapper mapper)
        {
            _prodottoBusiness = prodottoBusiness;
            _mapper = mapper;
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
        public async Task<ActionResult<Prodotto>> AddProdotto(ProdottoDTO prodottoDto)
        {
            try
            {
                var prodotto = _mapper.Map<Prodotto>(prodottoDto);
                var newProdotto = await _prodottoBusiness.AddProdottoAsync(prodotto);

                return CreatedAtAction(nameof(GetProdotto), new { id = newProdotto.Id }, newProdotto);

            } catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }   
        }


        // PUT: api/Prodotto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProdotto(int id, ProdottoDTO prodottoDto)
        {

            var prodotto = _mapper.Map<Prodotto>(prodottoDto);
            prodotto.Id = id; 
            var updateResult = await _prodottoBusiness.UpdateProdottoAsync(prodotto);

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
