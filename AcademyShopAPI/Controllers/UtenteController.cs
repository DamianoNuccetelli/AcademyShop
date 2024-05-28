﻿using System;
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
                var utentes = await _oBL.GetUtentes();
                return Ok(utentes);
        }

        // GET USER
        [HttpGet("{id}", Name = "GetUtente")]
        public async Task<ActionResult<Utente>> GetUtente(int id)
        {
                var utente = await _oBL.GetUtente(id);
                if (utente == null)
                {
                    return NotFound();
                }
                return utente;
        }

        // ADD USER
        [HttpPost]
        public async Task<ActionResult<Utente>> PostUtente(UtenteDTOperPOST utenteDTO)
        {
                // Esegue la mappatura dei dati da UtenteDto a Utente
                var utente = MapToUtente(utenteDTO);
                var result = await _oBL.PostUtente(utente);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<Utente>> DeleteUtente(int id)
        {
                var result = await _oBL.DeleteUtente(id);
                return result;
        }
    }
}
