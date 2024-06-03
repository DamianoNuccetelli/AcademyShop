﻿using AcademyShopAPI.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ManageUtenteData
    {
        private readonly IRepositoryUtente _utenteRepository;
        public ManageUtenteData( IRepositoryUtente utenteRepository)
        {
            _utenteRepository = utenteRepository;
        }

        //DANIEL 
        public async Task<IEnumerable<Utente>> GetUtentesAsync()
        {
            return await _utenteRepository.GetUtentesAsync();
        }

        public async Task<Utente> GetUtenteByIdAsync(int id)
        {
            return await _utenteRepository.GetUtenteByIdAsync(id);
        }

        public async Task<ActionResult<Utente>> AddUtenteAsync(Utente utente)
        {
            return await _utenteRepository.AddUtenteAsync(utente);
        }

        public async Task<ActionResult<Utente>> DeleteUtenteAsync(int id)
        {
            return await _utenteRepository.DeleteUtenteAsync(id);
        }
        public async Task<ActionResult<Utente>> DeleteUtenteEOrdineAsync(int id)
        {
            return await _utenteRepository.DeleteUtenteEOrdineAsync(id);
        }

        public async Task<bool> CheckUtenteExistsByEmailOrPassword(Utente utente)
        {
            return await _utenteRepository.CheckUtenteExistsByEmailOrPassword(utente);
        }

        public async Task<bool> CheckUtenteExistsById(int id)
        {
            return await _utenteRepository.CheckUtenteExistsById(id);
        }

        // Leonardo
        public async Task<Utente> LoginUser(string email, string password)
        {
            return await _utenteRepository.LoginUser(email, password);
        }

    }
}
