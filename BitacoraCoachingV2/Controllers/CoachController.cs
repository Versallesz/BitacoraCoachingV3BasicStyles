using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitacoraCoachingV2.Models;
using BitacoraCoachingV2.Models.TableViewModels;
using BitacoraCoachingV2.Controllers;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;

namespace BitacoraCoachingV2.Controllers
{
    public class CoachController : Controller
    {
        // GET: Coach
        public ActionResult Index()
        {
            List<AmbCoachTableViewModel> lst = null;
            using (COACHING6Entities db = new COACHING6Entities())
            {
                lst = (from d in db.Usuario
                       orderby d.nombre_usuario ascending
                       where d.Rol_id_rol == 2

                       select new AmbCoachTableViewModel
                       {
                           id_usuario = d.id_usuario,
                           email_usuario = d.email_usuario,
                           pass_usuario = d.pass_usuario,
                           nombre_usuario = d.nombre_usuario,
                           fono_usuario = d.fono_usuario,
                           Rol_id_rol = d.Rol_id_rol
                       }).Distinct().ToList();
            }
            return View(lst);

        }


        public static string Scoachs;
        public static string Sempresas;

        public ActionResult NuevoCoach()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult NuevoCoach(CoachTableViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (COACHING6Entities db = new COACHING6Entities())
                    {
                        var oUsuario = new Usuario();
                        oUsuario.email_usuario = model.email_usuario;
                        oUsuario.pass_usuario = model.pass_usuario;
                        oUsuario.nombre_usuario = model.nombre_usuario;
                        oUsuario.fono_usuario = model.fono_usuario;
                        oUsuario.Rol_id_rol = 2;
                        db.Usuario.Add(oUsuario);
                        db.SaveChanges();
                    }
                    return Redirect("~/Coach");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public ActionResult Editar(int id_usuario)
        {
            CoachTableViewModel model = new CoachTableViewModel();
            using (COACHING6Entities db = new COACHING6Entities())
            {
                var oUsuario = db.Usuario.Find(id_usuario);
                model.id_usuario = oUsuario.id_usuario;
                model.email_usuario = oUsuario.email_usuario;
                model.pass_usuario = oUsuario.pass_usuario;
                model.nombre_usuario = oUsuario.nombre_usuario;
                model.fono_usuario = oUsuario.fono_usuario;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Editar(CoachTableViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (COACHING6Entities db = new COACHING6Entities())
                    {
                        var oUsuario = db.Usuario.Find(model.id_usuario);
                        oUsuario.id_usuario = model.id_usuario;
                        oUsuario.email_usuario = model.email_usuario;
                        oUsuario.pass_usuario = model.pass_usuario;
                        oUsuario.nombre_usuario = model.nombre_usuario;
                        oUsuario.fono_usuario = model.fono_usuario;

                        db.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Coach");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Eliminar(int id_usuario)
        {
            CoachTableViewModel model = new CoachTableViewModel();
            using (COACHING6Entities db = new COACHING6Entities())
            {
                var oUsuario = db.Usuario.Find(id_usuario);
                db.Usuario.Remove(oUsuario);
                db.SaveChanges();
            }
            return Redirect("~/Coach");
        }


    }
}