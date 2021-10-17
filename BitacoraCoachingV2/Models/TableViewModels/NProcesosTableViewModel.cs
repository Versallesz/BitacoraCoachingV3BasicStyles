using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BitacoraCoachingV2.Models.TableViewModels
{
    public class NProcesosTableViewModel
    {
        //Proceso

       // [Required(ErrorMessage = "Este campo es obligatorio")]
       // [Display(Name = "Id")]
        public int id_proceso { get; set; }
        //public int id_proceso_coachee { get; set; }
        [Required]
        [Display(Name ="Fecha de inicio")]
        public System.DateTime fecha_inicio { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Indicador de éxito")]
        public string indicador_exito { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Plan de acción")]
        public string plan_accion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Objetivo")]
        public string objetivo_proceso { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Cantidad de sesiones")]
        public int cantidad_sesion { get; set; }

        [Display(Name = "Coach")]
        public int id_coach { get; set; }

        [Display(Name = "Coachee")]
        public int id_coachee { get; set; }
    }
}