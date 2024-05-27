using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.Dto
{
    public class UtenteDTOperPOST
    {
        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "La data di nascita è obbligatoria.")]
        public DateOnly DataNascita { get; set; }

        [Required(ErrorMessage = "La città di nascita è obbligatoria.")]
        public string CittaNascita { get; set; }

        [Required(ErrorMessage = "La provincia di nascita è obbligatoria.")]
        public string ProvinciaNascita { get; set; }

        [Required(ErrorMessage = "Il campo sesso è obbligatorio.")]
        public string Sesso { get; set; }

        [Required(ErrorMessage = "Il codice fiscale è obbligatorio.")]
        public string CodiceFiscale { get; set; }

        [Required(ErrorMessage = "L'email è obbligatoria.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria.")]
        public string Password { get; set; }
    }
}
