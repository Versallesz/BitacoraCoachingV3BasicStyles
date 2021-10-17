using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitacoraCoachingV2.Models.TableViewModels
{
    public class AmbCoachTableViewModel
    {

        //Proceso
        public int id_proceso { get; set; }
        public int id_proceso_coachee { get; set; }
        public System.DateTime fecha_inicio { get; set; }
        public string indicador_exito { get; set; }
        public string plan_accion { get; set; }
        public string objetivo_proceso { get; set; }
        public string documento_proceso { get; set; }
        public int cantidad_sesion { get; set; }

        //usuario
        public int id_usuario { get; set; }
        public string email_usuario { get; set; }
        public string pass_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string nombre_usuario_coachee { get; set; }
        public string fono_usuario { get; set; }
        public string cargo_usuario { get; set; }
        public int Rol_id_rol { get; set; }
        public int? Empresa_id_empresa { get; set; }

        //empresa
        public int id_empresa { get; set; }
        public string nombre_empresa { get; set; }
        public string nombre_empresa_coachee { get; set; }
        public string cargo_coachee { get; set; }

        //area
        public int id_area { get; set; }
        public string nombre_area { get; set; }
        public string shortname { get; set; }

        //proceso_usuario
        public int idp { get; set; }
        public int Proceso_id_proceso { get; set; }
        public int Usuario_id_usuario { get; set; }

        //Sesiones
        public int id_sesion { get; set; }
        public System.DateTime fecha_sesion { get; set; }
        public string descripcion_sesion { get; set; }
        public string estado_sesion { get; set; }
        public string documento_sesion { get; set; }

    }
}