﻿using AcademyShopAPI.Models;
using DataLayer;
using DataLayer.Repository;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ManageUtenteBusiness
    {
        private readonly IRepositoryWithDtoAsync<Utente, UtenteDTO> _repo;
        private readonly ManageUtenteData oUDL;

        
        public ManageUtenteBusiness(IRepositoryWithDtoAsync<Utente, UtenteDTO> repo, ManageUtenteData oUDL)
        {
            _repo = repo;
            this.oUDL = oUDL;
        }

        public async Task<IEnumerable<UtenteDTO>> GetAllUtentiAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<UtenteDTO> GetUtenteDetailsAsync(int id)
        {
            return await _repo.GetDetailsAsync(id);
        }

        //DANIEL ADD UTENTE 
        public async Task<ActionResult<Utente>> AddUtenteAsync(Utente utente)
        {
            try
            {
                // Verifico se un utente è gia esistente
                if (await oUDL.CheckUtenteExists(utente))
                {
                    return ErrorContentResult("Un utente con lo stesso codice fiscale o email esiste già.", 409);
                }

                // Controllo tramite regex del codice fiscale
                if (!IsValidCodiceFiscale(utente.CodiceFiscale))
                {
                    return ErrorContentResult("Un utente con lo stesso codice fiscale o email esiste già.");
                }

                // Controllo tramite regex dell'email
                if (!IsValidEmail(utente.Email))
                {
                    return ErrorContentResult("L'email non è valida.");
                }

                // Controllo sulla data di nascita (l'utente deve essere nato tra il 1900 e il giorno d'oggi)
                if (!IsValidBirthDate(utente.DataNascita))
                {
                    return ErrorContentResult("La data di nascita non è valida.");
                }


                if (utente.CodiceFiscale.Length != 16 || utente.Password.Length != 16 || utente.ProvinciaNascita.Length != 2 || (utente.Sesso != "M" && utente.Sesso != "F"))
                {
                    return ErrorContentResult("Uno o più campi non rispettano la lunghezza o i valori richiesti.");
                }

                return await oUDL.AddUtenteAsync(utente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante la creazione dell'utente: {ex.Message}");
            }
        }
        //CRUD
        public async Task<bool> UpdateUtenteAsync(UtenteDTO utente)
        {
            return await _repo.UpdateAsync(utente);
        }
        //CRUD
        public async Task<bool> DeleteUtenteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        //DANIEL 
        private ContentResult ErrorContentResult(string errorMessage, int statusCode = 400)
        {
            return new ContentResult
            {
                Content = errorMessage,
                ContentType = "text/plain",
                StatusCode = statusCode
            };
        }
        //Daniel
        private bool IsValidCodiceFiscale(string codiceFiscale)
        {
            string regexPattern = @"^[A-Z]{6}\d{2}[ABCDEHLMPRST]\d{2}[A-Z]\d{3}[A-Z]$";
            return System.Text.RegularExpressions.Regex.IsMatch(codiceFiscale, regexPattern);
        }

        private bool IsValidEmail(string email)
        {
            string regexPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, regexPattern);
        }

        private bool IsValidBirthDate(DateOnly birthDate)
        {
            DateOnly minDate = new DateOnly(1900, 1, 1);
            DateOnly maxDate = DateOnly.FromDateTime(DateTime.UtcNow);
            return birthDate >= minDate && birthDate <= maxDate;
        }

    }
}
