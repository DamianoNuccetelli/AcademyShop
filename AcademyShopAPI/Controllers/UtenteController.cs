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

namespace ProgettoAcademyShop.Controller
{
    //Task Daniel Roberti -> Aggiunta ed eliminazione utente dal db

    [Route("api/[controller]")]
    [ApiController]
    public class UtenteController : ControllerBase
    {
        private readonly BusinessLayer.ManageBusiness _oBL;

        public UtenteController(BusinessLayer.ManageBusiness oBL)
        {
            _oBL = oBL;
        }

        // GET USERS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utente>>> GetUtentes()
        {
            try
            {
                var utentes = await _oBL.GetUtentes();
                return Ok(utentes);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Errore durante il recupero degli utenti: {ex.Message}");
            }
        }

        // GET USER
        [HttpGet("{id}", Name = "GetUtente")]
        public async Task<ActionResult<Utente>> GetUtente(int id)
        {
            try
            {
                var utente = await _oBL.GetUtente(id);
                if (utente == null)
                {
                    return NotFound();
                }
                return utente;
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Errore durante il recupero dell'utente con ID {id}: {ex.Message}");
            }
        }

        // ADD USER
        [HttpPost]
        public async Task<ActionResult<Utente>> PostUtente(UtenteDTOperPOST utenteDTO)
        {
            try
            {
                // Esegue la mappatura dei dati da UtenteDto a Utente
                var utente = MapToUtente(utenteDTO);
                var result = await _oBL.PostUtente(utente);
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Errore durante la creazione dell'utente: {ex.Message}");
            }
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUtente(int id)
        {
            try
            {
                var result = await _oBL.DeleteUtente(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Errore durante l'eliminazione dell'utente con ID {id}: {ex.Message}");
            }
        }
    }
}
