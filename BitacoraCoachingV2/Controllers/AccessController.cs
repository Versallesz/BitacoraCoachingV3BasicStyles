using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitacoraCoachingV2.Models;

namespace BitacoraCoachingV2.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        public static long valorid;
        public ActionResult Enter(string user, string password)
        {

            try
            {
                using (COACHING6Entities db = new COACHING6Entities())
                {
                    var lst = from d in db.Usuario
                              where d.email_usuario == user && d.pass_usuario == password
                              select d;
                    if (lst.Count() > 0)
                    {
                        Usuario oUser = lst.First();
                        Session["User"] = oUser;
                        valorid = oUser.id_usuario;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario y/o Contraseña incorrectos o inexistentes");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("Ocurrió un error" + ex.Message);
            }
        }
    }
}