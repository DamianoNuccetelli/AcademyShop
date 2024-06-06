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
        //Daniel
        Task<IEnumerable<Utente>> GetUtentesAsync();
        Task<Utente> GetUtenteByIdAsync(int id);
        Task<ActionResult<Utente>> AddUtenteAsync(Utente utente);
        ActionResult<Utente> DeleteUtenteSync(int id);
        ActionResult<Utente> DeleteUtenteEOrdine(int id);
        Task<bool> CheckUtenteExistsByEmailOrPassword(Utente utente);
        Task<bool> CheckUtenteExistsById(int id);
        //Leonardo
        Task<Utente> LoginUser(string email, string password);

        
    }
}
