using AcademyShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRepositoryUtente
    {
        //-------------Daniel---------------//
        Task<IEnumerable<Utente>> GetUtentesAsync();
        Task<Utente> GetUtenteByIdAsync(int id);
        Task<ActionResult<Utente>> AddUtenteAsync(Utente utente);
        Task<ActionResult<Utente>> DeleteUtenteAsync(int id);
        Task<bool> CheckUtenteExistsByEmailOrPassword(Utente utente);
        Task<bool> CheckUtenteExistsById(int id);
        Task<IEnumerable<Utente>> GetUtentes();
        Task<Utente> GetUtente(int id);
        Task<IEnumerable<Utente>> GetAllAsync();
        Task<Utente> GetByIdAsync(int id);
        Task<Utente> AddAsync(Utente utente);
        Task<Utente> UpdateUtenteAsync(Utente utente);
        Task<bool> DeleteAsync(int id);

        //Leonardo
        Task<Utente> LoginUser(string email, string password);


    }
}
