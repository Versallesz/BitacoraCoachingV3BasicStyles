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
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {

            List<AmbCoachTableViewModel> lst = null;
            List<AmbCoachTableViewModel> lst3 = null;


            using (COACHING6Entities db = new COACHING6Entities())
            {
                lst = (from d in db.Usuario
                       join em in db.Empresa
                       on d.Empresa_id_empresa equals em.id_empresa
                       orderby d.nombre_usuario ascending

                       select new AmbCoachTableViewModel
                       {
                           id_usuario = d.id_usuario,
                           email_usuario = d.email_usuario,
                           pass_usuario = d.pass_usuario,
                           nombre_usuario = d.nombre_usuario,
                           fono_usuario = d.fono_usuario,
                           cargo_usuario = d.cargo_usuario,
                           Rol_id_rol = d.Rol_id_rol,
                           Empresa_id_empresa = d.Empresa_id_empresa,
                           id_empresa = em.id_empresa,
                           nombre_empresa = em.nombre_empresa
                       }).Distinct().ToList();
            }


            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst3 = (from d in db2.Empresa
                        orderby d.nombre_empresa ascending
                        select new AmbCoachTableViewModel
                        {
                            id_empresa = d.id_empresa,
                            nombre_empresa = d.nombre_empresa
                        }).ToList();

            }
            List<SelectListItem> items2 = lst3.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre_empresa.ToString(),
                    Value = d.id_empresa.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items2 = items2;

            return View(lst);

        }


        public static string Scoachs;
        public static string Sempresas;


        public ActionResult Buscar(string empresas)
        {
            var iddetectada = AccessController.valorid;
            List<AmbCoachTableViewModel> lst = null;
            List<AmbCoachTableViewModel> lst3 = null;

            Sempresas = empresas;

            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst3 = (from d in db2.Empresa
                        orderby d.nombre_empresa ascending
                        select new AmbCoachTableViewModel
                        {
                            id_empresa = d.id_empresa,
                            nombre_empresa = d.nombre_empresa
                        }).ToList();

            }
            List<SelectListItem> items2 = lst3.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre_empresa.ToString(),
                    Value = d.id_empresa.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items2 = items2;


            using (COACHING6Entities db = new COACHING6Entities())
            {
                var companies = new List<int>();
                foreach (int id_empresa in lst3.Select(x => x.id_empresa).Distinct().ToList())
                {
                    companies.Add(id_empresa);
                }


                if (empresas == "")
                {

                    lst = (from d in db.Usuario
                           join em in db.Empresa
                           on d.Empresa_id_empresa equals em.id_empresa
                           orderby d.nombre_usuario ascending
                           where (companies.Contains(em.id_empresa))

                           select new AmbCoachTableViewModel
                           {
                               id_usuario = d.id_usuario,
                               email_usuario = d.email_usuario,
                               pass_usuario = d.pass_usuario,
                               nombre_usuario = d.nombre_usuario,
                               fono_usuario = d.fono_usuario,
                               cargo_usuario = d.cargo_usuario,
                               Rol_id_rol = d.Rol_id_rol,
                               Empresa_id_empresa = d.Empresa_id_empresa,
                               id_empresa = em.id_empresa,
                               nombre_empresa = em.nombre_empresa
                           }).Distinct().ToList();
                }
                else
                {
                    lst = (from d in db.Usuario
                           join em in db.Empresa
                           on d.Empresa_id_empresa equals em.id_empresa
                           orderby d.nombre_usuario ascending
                           where ((d.Empresa_id_empresa).ToString() == empresas)

                           select new AmbCoachTableViewModel
                           {
                               id_usuario = d.id_usuario,
                               email_usuario = d.email_usuario,
                               pass_usuario = d.pass_usuario,
                               nombre_usuario = d.nombre_usuario,
                               fono_usuario = d.fono_usuario,
                               cargo_usuario = d.cargo_usuario,
                               Rol_id_rol = d.Rol_id_rol,
                               Empresa_id_empresa = d.Empresa_id_empresa,
                               id_empresa = em.id_empresa,
                               nombre_empresa = em.nombre_empresa
                           }).Distinct().ToList();

                }

                return View("Index", lst);
            }
        }



        public ActionResult NuevoUsuario()
        {
            //DROP EMPRESAS
            List<AmbCoachTableViewModel> lst3 = null;
            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst3 = (from d in db2.Empresa
                        orderby d.nombre_empresa ascending
                        select new AmbCoachTableViewModel
                        {
                            id_empresa = d.id_empresa,
                            nombre_empresa = d.nombre_empresa
                        }).ToList();

            }
            List<SelectListItem> items2 = lst3.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre_empresa.ToString(),
                    Value = d.id_empresa.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items2 = items2;
            //DROP EMPRESAS

            return View();
        }
        //AREA
        [HttpGet]
        public JsonResult Jefe(int idCompany)
        {
            List<ElemenJsontIntKey> lst3 = new List<ElemenJsontIntKey>();
            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst3 = (from d in db2.Jefe
                        where d.Empresa_id_empresa == idCompany
                        orderby d.nombre_jefe ascending
                        select new ElemenJsontIntKey
                        {
                            value = d.id_jefe,
                            text = d.nombre_jefe
                        }).ToList();

            }return Json(lst3, JsonRequestBehavior.AllowGet);
        }
        public class ElemenJsontIntKey
        {
            public int value { get; set; }
            public string text { get; set; }
        }

        //AREA

        [HttpPost]
        public ActionResult NuevoUsuario(UsuariosTableViewModel model, int area)
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
                        oUsuario.cargo_usuario= model.cargo_usuario;
                        oUsuario.Rol_id_rol = 3;
                        oUsuario.Empresa_id_empresa = model.Empresa_id_empresa;
                        oUsuario.Jefe_id_jefe = area;
                        db.Usuario.Add(oUsuario);
                        db.SaveChanges();
                    }
                    return Redirect("~/Usuarios");
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

            //DROP EMPRESAS
            List<AmbCoachTableViewModel> lst3 = null;
            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst3 = (from d in db2.Empresa
                        orderby d.nombre_empresa ascending
                        select new AmbCoachTableViewModel
                        {
                            id_empresa = d.id_empresa,
                            nombre_empresa = d.nombre_empresa
                        }).ToList();

            }
            List<SelectListItem> items2 = lst3.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre_empresa.ToString(),
                    Value = d.id_empresa.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items2 = items2;


            UsuariosTableViewModel model = new UsuariosTableViewModel();
            using (COACHING6Entities db = new COACHING6Entities()) 
            {
                var oUsuario = db.Usuario.Find(id_usuario);
                model.id_usuario= oUsuario.id_usuario;
                model.email_usuario= oUsuario.email_usuario;
                model.pass_usuario= oUsuario.pass_usuario;
                model.nombre_usuario= oUsuario.nombre_usuario;
                model.fono_usuario= oUsuario.fono_usuario;
                model.cargo_usuario= oUsuario.cargo_usuario;
               // model.Rol_id_rol= oUsuario.Rol_id_rol;
                model.Empresa_id_empresa = oUsuario.Empresa_id_empresa;
                model.Jefe_id_jefe = oUsuario.Jefe_id_jefe;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Editar(UsuariosTableViewModel model, int area)
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
                        oUsuario.cargo_usuario = model.cargo_usuario;
                       // oUsuario.Rol_id_rol = model.Rol_id_rol;
                        oUsuario.Empresa_id_empresa = model.Empresa_id_empresa;
                        oUsuario.Jefe_id_jefe = area;

                        db.Entry(oUsuario).State= System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Usuarios");
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
            UsuariosTableViewModel model = new UsuariosTableViewModel();
            using (COACHING6Entities db = new COACHING6Entities())
            {
                var oUsuario = db.Usuario.Find(id_usuario);
                db.Usuario.Remove(oUsuario);
                db.SaveChanges();
            }
            return Redirect("~/Usuarios");
        }


    }
}
