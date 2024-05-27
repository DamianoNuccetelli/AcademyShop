using AcademyShopAPI.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoLayer.Dto;
using Microsoft.AspNetCore.Mvc;


namespace BusinessLayer
{
    public class ManageBusiness
    {

        private readonly ManageData oDL;

        public ManageBusiness(ManageData _oDL)
        {
            oDL = _oDL;
        }
        //Florea chiama dataLayer
        public async Task<int?> UtenteExists(int id)
        {
            try
            {
                return await oDL.UtenteExists(id);
            }

            catch (Exception ex)
            {
                throw new Exception("errore durante recupero id utente nel business layer", ex);
            }
        }

            //Renato Florea chiamata al DataLayer
            public async Task<List<OrdiniByIdUserDTO>> GetOrdiniByUserId(int userId)
        {

            try
            {
                    // Chiama il metodo corrispondente del data layer per recuperare l'id dell'utente
                    return await oDL.GetOrdiniByUserId(userId);            
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui 
                throw new Exception("Errore durante il recupero dell'ID dell'utente nel business layer.", ex);
            }
        }
       

        public async Task<int?> RecuperaIdOrdineAsync(int idUtente, int idDettaglioOrdine)
        {
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaIdOrdineAsync(idUtente, idDettaglioOrdine);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'ID dell'ordine nel business layer.", ex);
            }
        }


        public async Task<int?> RecuperaStatoOrdineAsync(int idOrdineEsistente)
        {
           // oDL = new ManageData(_context);
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaStatoOrdineAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'ID dello stato dell'ordine nel business layer.", ex);
            }
        }

        public async Task<int?> RecuperaQuantitaProdottoAsync(int idOrdineEsistente)
        {
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaQuantitaProdottoAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero della quantita del prodotto nel business layer.", ex);
            }
        }

        public async Task<int?> RecuperaIdProdottoAsync(int idOrdineEsistente)
        {
            try
            {
                // Chiama il metodo corrispondente del data layer per recuperare l'ID dell'ordine
                return await oDL.RecuperaIdProdottoAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                // Gestisci eventuali errori qui o riportali al chiamante
                throw new Exception("Errore durante il recupero dell'ID del prodotto nel business layer.", ex);
            }
        }

        public async Task<bool> ModificaOrdineAsync(int idOrdineEsistente, int idProdotto, int quantita)
        {
            try
            {
                return await oDL.ModificaOrdineAsync(idOrdineEsistente, idProdotto, quantita);
            }
            catch (Exception ex)
            {
                // Gestisci l'errore, ad esempio registrandolo o rilanciandolo
                throw new Exception("Errore durante la modifica dell'ordine nel business layer.", ex);
            }
        }

        public async Task<bool> DeleteOrdineAsync(int idOrdineEsistente)
        {
            try
            {
                return await oDL.DeleteOrdineAsync(idOrdineEsistente);
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la cancellazione dell'ordine nel business layer.", ex);
            }
        }


        public async Task<OrdineDettaglioDTOperGET> GetOrdineDettaglioAsync(int userId, int dettaglioOrdineId)
        {
            var userExists = await oDL.GetUtente(userId) != null;

            if (userId <= 0 || dettaglioOrdineId <= 0 || !userExists)
            {
                throw new ApplicationException("I parametri userId e dettaglioOrdineId sono errati");          
            }

          
            return await oDL.GetOrdineDettaglioAsync(userId, dettaglioOrdineId);
        }

        //Daniel -> Aggiunta e rimozione dell'utente dal db
        public async Task<IEnumerable<Utente>> GetUtentes()
        {
            return await oDL.GetUtentes();
        }

        public async Task<Utente> GetUtente(int id)
        {
            return await oDL.GetUtente(id);
        }

        public async Task<ActionResult<Utente>> PostUtente(Utente utente)
        {
            try
            {
                // Controllo tramite regex del codice fiscale
                if (!IsValidCodiceFiscale(utente.CodiceFiscale))
                {
                    return new ContentResult
                    {
                        Content = "Il Codice Fiscale non è valido.",
                        ContentType = "text/plain",
                        StatusCode = 400
                    };
                }

                // Controllo tramite regex dell'email
                if (!IsValidEmail(utente.Email))
                {
                    return new ContentResult
                    {
                        Content = "L'email non è valida.",
                        ContentType = "text/plain",
                        StatusCode = 400
                    };
                }

                // Controllo sulla data di nascita (l'utente deve essere nato tra il 1900 e il giorno d'oggi)
                if (!IsValidBirthDate(utente.DataNascita))
                {
                    return new ContentResult
                    {
                        Content = "La data di nascita non è valida.",
                        ContentType = "text/plain",
                        StatusCode = 400
                    };
                }

                // Controlli di business (al posto di questi controlli utilizzo il campo required sul dto)
                //if (string.IsNullOrWhiteSpace(utente.Cognome) || string.IsNullOrWhiteSpace(utente.Nome) ||
                //    string.IsNullOrWhiteSpace(utente.CodiceFiscale) || string.IsNullOrWhiteSpace(utente.Password) ||
                //    string.IsNullOrWhiteSpace(utente.CittaNascita) || string.IsNullOrWhiteSpace(utente.ProvinciaNascita) ||
                //    string.IsNullOrWhiteSpace(utente.Sesso) || string.IsNullOrWhiteSpace(utente.Email))
                //{
                //    return new ContentResult
                //    {
                //        Content = "Tutti i campi sono obbligatori.",
                //        ContentType = "text/plain",
                //        StatusCode = 400
                //    };
                //}

                if (utente.CodiceFiscale.Length != 16 || utente.Password.Length != 16 || utente.ProvinciaNascita.Length != 2 || (utente.Sesso != "M" && utente.Sesso != "F"))
                {
                    return new ContentResult
                    {
                        Content = "Uno o più campi non rispettano la lunghezza o i valori richiesti.",
                        ContentType = "text/plain",
                        StatusCode = 400
                    };
                }

                return await oDL.PostUtente(utente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante la creazione dell'utente: {ex.Message}");
            }
        }

        public async Task<string> DeleteUtente(int id)
        {
            return await oDL.DeleteUtente(id);
        }

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

        //Adriano
        public async Task<int> NuovoOrdine(int idUtente, int idprodotto, int quantità)
        {
            int idOrdine;

          
            Prodotto prodotto = await oDL.GetProdottoAsync(idprodotto);

            if (prodotto != null && quantità != 0
                && prodotto.Quantità >= quantità)
            {
                prodotto.Quantità -= quantità;

                idOrdine = await oDL.NuovoOrdine(idUtente, prodotto, quantità);
                return idOrdine;
            }
            else
            {// codice errore 400
                throw new ArgumentException();
            }
        }
        }
    }

