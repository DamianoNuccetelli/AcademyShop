using AcademyShopAPI.Models;
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
        private readonly AcademyShopDBContext _context;
        private readonly IRepositoryUtente _utenteRepository;

        public ManageUtenteData(AcademyShopDBContext context, IRepositoryUtente utenteRepository)
        {
            _context = context;
            _utenteRepository = utenteRepository;
        }

        ////DANIEL CON REPOSITORY

        public async Task<ActionResult<Utente>> AddUtenteAsync(Utente utente)
        {
            return await _utenteRepository.AddUtenteAsync(utente);
        }

        public async Task<ActionResult<Utente>> DeleteUtente(int id)
        {
            return await _utenteRepository.DeleteUtente(id);
        }

        public async Task<bool> CheckUtenteExists(Utente utente)
        {
            return await _utenteRepository.CheckUtenteExists(utente);
        }

        public async Task<bool> CheckUtenteExistsById(int id)
        {
            return await _utenteRepository.CheckUtenteExistsById(id);
        }

        public async Task<IEnumerable<Utente>> GetUtentes()
        {
            return await _utenteRepository.GetUtentes();
        }

        public async Task<Utente> GetUtente(int id)
        {
            return await _utenteRepository.GetUtente(id);
        }

        public async Task<IEnumerable<Utente>> GetAllAsync()
        {
            return await _utenteRepository.GetAllAsync();
        }

        public async Task<Utente> GetByIdAsync(int id)
        {
            return await _utenteRepository.GetByIdAsync(id);
        }

        public async Task<Utente> AddAsync(Utente utente)
        {
            return await _utenteRepository.AddAsync(utente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _utenteRepository.DeleteAsync(id);
        }

       public async Task<Utente> UpdateUtenteAsync(Utente utente)
        {
            return await _utenteRepository.UpdateUtenteAsync(utente);
        }


        // Leonardo
        public async Task<Utente> LoginUser(string email, string password)
        {
            return await _utenteRepository.LoginUser(email, password);
        }



    }
}
