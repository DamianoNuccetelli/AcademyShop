using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademyShopAPI.Models;
using DtoLayer;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Identity.Data;
using BusinessLayer;

namespace ProgettoAcademyShop.Controller
{
    //Daniel
    //[Route("api/[controller]")]
    [ApiController]
    public class UtenteController : ControllerBase
    {
        private readonly ManageUtenteBusiness _oUBL;

        public UtenteController(ManageUtenteBusiness oUBL)
        {
            _oUBL = oUBL;
        }

        // GET USERS
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<Utente>>> GetUtentesAsync()
        {
                var utentes = await _oUBL.GetUtentesAsync();
                return Ok(utentes);
        }

        // GET USER
        [HttpGet("users/{id}")]
        public async Task<ActionResult<Utente>> GetUtenteByIdAsync(int id)
        {
                var utente = await _oUBL.GetUtenteByIdAsync(id);
                if (utente == null)
                {
                    return NotFound();
                }
                return utente;
        }

        // ADD USER
        [HttpPost("users")]
        public async Task<ActionResult<Utente>> AddUtenteAsync(UtenteDTOperPOST utenteDTO)
        {
                // Esegue la mappatura dei dati da UtenteDto a Utente
                var utente = MapToUtente(utenteDTO);
                var result = await _oUBL.AddUtenteAsync(utente);
                return result;
        }


        // Non gli passo id e data di registrazione
        private Utente MapToUtente(UtenteDTOperPOST utenteDto)
        {
            return new Utente
            {
                Cognome = utenteDto.Cognome,
                Nome = utenteDto.Nome,
                DataNascita = utenteDto.DataNascita,
                CittaNascita = utenteDto.CittaNascita,
                ProvinciaNascita = utenteDto.ProvinciaNascita,
                Sesso = utenteDto.Sesso,
                CodiceFiscale = utenteDto.CodiceFiscale,
                Email = utenteDto.Email,
                Password = utenteDto.Password
            };
        }

        // DELETE USER
        [HttpDelete("users/{id}")]
        public ActionResult<Utente> DeleteUtenteSync(int id)
        {
            var result =  _oUBL.DeleteUtenteAsync(id);
            return result;
        }


        // Leonardo
        [HttpPost("managed-users")]
        public async Task<ActionResult<int>> Login(string email, string password)
        {
            var result = await _oUBL.LoginUser(email, password);
            return result.Value > 0 ? Ok(new { id = result.Value })  : result;
        }
    }
}
