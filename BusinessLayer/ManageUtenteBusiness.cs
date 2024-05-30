using AcademyShopAPI.Models;
using DataLayer;
using DataLayer.Repository;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Http;
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
        

        
        public ManageUtenteBusiness(IRepositoryWithDtoAsync<Utente, UtenteDTO> repo, ManageUtenteData _oUDL)
        {
            _repo = repo;
            oUDL = _oUDL;
        }

        public async Task<IEnumerable<Utente>> GetAllUtentiAsync()
        {
            return await oUDL.GetAllAsync();
        }

        public async Task<Utente> GetUtenteDetailsAsync(int id)
        {
            return await oUDL.GetByIdAsync(id);
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

        public async Task<bool> UpdateUtenteAsync(UtenteDTO utente)
        {
            return await _repo.UpdateAsync(utente);
        }

        public async Task<ActionResult<bool>> DeleteUtenteAsync(int id)
        {
            try
            {
                var result = await _repo.DeleteAsync(id);
                if (!result)
                {
                    return ErrorContentResult("Errore: Utente non trovato.", 500);
                }

                return new OkObjectResult("Utente eliminato con successo.");
            }
            catch (Exception ex)
            {
                
                return ErrorContentResult($"Errore durante la cancellazione dell'utente: {ex.Message}", 500);
            }
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

        //Daniel -> Aggiunta e rimozione dell'utente dal db
        public async Task<IEnumerable<Utente>> GetUtentes()
        {
            return await oUDL.GetUtentes();
        }

        public async Task<Utente> GetUtente(int id)
        {
            return await oUDL.GetUtente(id);
        }
        // Leonardo
        public async Task<ActionResult<int>> LoginUser(string email, string password)
        {
            try
            {
                Utente utente = await oUDL.LoginUser(email, password);

                if (utente != null)
                {
                    return utente.Id;
                }
                else
                {
                    return ErrorContentResult("Client Error. \nEmail o Password non corretta.", 404);
                }
            }
            catch (Exception ex)
            {
                return ErrorContentResult("Server Error.\nSi è verificato un errore durante l'accesso del database", 500);
            }
        }
    }
}
