using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BitacoraCoachingV2.Models.TableViewModels
{
    public class UsuariosTableViewModel
    {
        //usuario
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        //[Display(Name = "Id")]
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
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Cargo")]
        public string cargo_usuario { get; set; }
       // [Required(ErrorMessage = "Este campo es obligatorio")]
       // [Display(Name = "Rol")]
       // public int Rol_id_rol { get; set; }
        

        [Display(Name = "Empresa")]
        public int? Empresa_id_empresa { get; set; }

        [Display(Name = "Jefe")]
        public int? Jefe_id_jefe { get; set; }


    }
}