using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BitacoraCoachingV2.Models.TableViewModels
{
    public class CoachTableViewModel
    {
        public int id_usuario { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Correo Electrónico")]
        public string email_usuario { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Contraseña")]
        public string pass_usuario { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Nombre")]
        public string nombre_usuario { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Teléfono")]
        public string fono_usuario { get; set; }

    }
}