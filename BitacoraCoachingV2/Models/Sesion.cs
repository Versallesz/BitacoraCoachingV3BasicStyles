//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BitacoraCoachingV2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sesion
    {
        public int id_sesion { get; set; }
        public System.DateTime fecha_sesion { get; set; }
        public string descripcion_sesion { get; set; }
        public string estado_sesion { get; set; }
        public string documento_sesion { get; set; }
        public int Proceso_id_proceso { get; set; }
    
        public virtual Proceso Proceso { get; set; }
    }
}