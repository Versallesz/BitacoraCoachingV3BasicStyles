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
    public class ProcesosController : Controller
    {
        // GET: Procesos
        public ActionResult Index()
        {

            List<ProcesosTableViewModel> lst = null;
            List<ProcesosTableViewModel> lstcoachee = null;
            List<ProcesosTableViewModel> lst2 = null;
            List<ProcesosTableViewModel> lst3 = null;


            using (COACHING6Entities db = new COACHING6Entities())
            {
                lst = (from d in db.Proceso_usuario
                           join v in db.Proceso
                           on d.Proceso_id_proceso equals v.id_proceso
                           join e in db.Usuario
                           on d.Usuario_id_usuario equals e.id_usuario
                           orderby v.id_proceso ascending
                           where (e.Rol_id_rol == 2)

                           select new ProcesosTableViewModel
                           {
                               id_proceso = v.id_proceso,
                               fecha_inicio = v.fecha_inicio,
                               indicador_exito = v.indicador_exito,
                               plan_accion = v.plan_accion,
                               objetivo_proceso = v.objetivo_proceso,
                               cantidad_sesion = v.cantidad_sesion,
                               id_usuario = e.id_usuario,
                               email_usuario = e.email_usuario,
                               pass_usuario = e.pass_usuario,
                               nombre_usuario = e.nombre_usuario,
                               Rol_id_rol = e.Rol_id_rol,
                               idp = d.idp,
                               Proceso_id_proceso = d.Proceso_id_proceso,
                               Usuario_id_usuario = d.Usuario_id_usuario
                           }).Distinct().ToList();
                lstcoachee = (from d in db.Proceso_usuario
                       join v in db.Proceso
                       on d.Proceso_id_proceso equals v.id_proceso
                       join e in db.Usuario
                       on d.Usuario_id_usuario equals e.id_usuario
                       join em in db.Empresa
                       on e.Empresa_id_empresa equals em.id_empresa
                       orderby v.id_proceso ascending
                       where (e.Rol_id_rol == 3)

                       select new ProcesosTableViewModel
                       {
                           id_proceso_coachee = v.id_proceso,
                           nombre_usuario_coachee = e.nombre_usuario,
                           cargo_coachee = e.cargo_usuario,
                           nombre_empresa_coachee = em.nombre_empresa                
                       }).Distinct().ToList();

            }

            lst.AddRange(lstcoachee);
            //probando dropdowns
            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst2 = (from d in db2.Usuario
                        orderby d.nombre_usuario ascending
                        where d.Rol_id_rol == 2
                        select new ProcesosTableViewModel
                        {
                            id_usuario = d.id_usuario,
                            nombre_usuario = d.nombre_usuario
                        }).ToList();

            }
            List<SelectListItem> items = lst2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre_usuario.ToString(),
                    Value = d.id_usuario.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items = items;

            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst3 = (from d in db2.Empresa
                        orderby d.nombre_empresa ascending
                        select new ProcesosTableViewModel
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


        public ActionResult Buscar(string coachs, string empresas)
        {
            var iddetectada = AccessController.valorid;
            List<ProcesosTableViewModel> lst = null;
            List<ProcesosTableViewModel> lstcoachee = null;
            List<ProcesosTableViewModel> lst2 = null;
            List<ProcesosTableViewModel> lst3 = null;

            Scoachs = coachs;
            Sempresas = empresas;


            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst2 = (from d in db2.Usuario
                        orderby d.nombre_usuario ascending
                        where d.Rol_id_rol == 2
                        select new ProcesosTableViewModel
                        {
                            id_usuario = d.id_usuario,
                            nombre_usuario = d.nombre_usuario
                        }).ToList();

            }
            List<SelectListItem> items = lst2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre_usuario.ToString(),
                    Value = d.id_usuario.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items = items;

            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst3 = (from d in db2.Empresa
                        orderby d.nombre_empresa ascending
                        select new ProcesosTableViewModel
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
                var coachlist = new List<int>();
                foreach (int id_usuario in lst2.Select(x => x.id_usuario).Distinct().ToList())
                {
                    coachlist.Add(id_usuario);
                }


                if (coachs == "")
                {

                    lst = (from d in db.Proceso_usuario
                           join v in db.Proceso
                           on d.Proceso_id_proceso equals v.id_proceso
                           join e in db.Usuario
                           on d.Usuario_id_usuario equals e.id_usuario
                           orderby v.id_proceso ascending
                           where (coachlist.Contains(e.id_usuario) && e.Rol_id_rol == 2)

                           select new ProcesosTableViewModel
                           {
                               id_proceso = v.id_proceso,
                               fecha_inicio = v.fecha_inicio,
                               indicador_exito = v.indicador_exito,
                               plan_accion = v.plan_accion,
                               objetivo_proceso = v.objetivo_proceso,
                               cantidad_sesion = v.cantidad_sesion,
                               id_usuario = e.id_usuario,
                               email_usuario = e.email_usuario,
                               pass_usuario = e.pass_usuario,
                               nombre_usuario = e.nombre_usuario,
                               Rol_id_rol = e.Rol_id_rol,
                               idp = d.idp,
                               Proceso_id_proceso = d.Proceso_id_proceso,
                               Usuario_id_usuario = d.Usuario_id_usuario
                           }).Distinct().ToList();
                    lstcoachee = (from d in db.Proceso_usuario
                                  join v in db.Proceso
                                  on d.Proceso_id_proceso equals v.id_proceso
                                  join e in db.Usuario
                                  on d.Usuario_id_usuario equals e.id_usuario
                                  join em in db.Empresa
                                  on e.Empresa_id_empresa equals em.id_empresa
                                  orderby v.id_proceso ascending
                                  where (e.Rol_id_rol == 3)

                                  select new ProcesosTableViewModel
                                  {
                                      id_proceso_coachee = v.id_proceso,
                                      nombre_usuario_coachee = e.nombre_usuario,
                                      cargo_coachee = e.cargo_usuario,
                                      nombre_empresa_coachee = em.nombre_empresa
                                  }).Distinct().ToList();
                    lst.AddRange(lstcoachee);


                }





                else
                {
                    lst = (from d in db.Proceso_usuario
                           join v in db.Proceso
                           on d.Proceso_id_proceso equals v.id_proceso
                           join e in db.Usuario
                           on d.Usuario_id_usuario equals e.id_usuario
                           orderby v.id_proceso ascending
                           where ((e.id_usuario).ToString() == coachs && e.Rol_id_rol == 2)

                           select new ProcesosTableViewModel
                           {
                               id_proceso = v.id_proceso,
                               fecha_inicio = v.fecha_inicio,
                               indicador_exito = v.indicador_exito,
                               plan_accion = v.plan_accion,
                               objetivo_proceso = v.objetivo_proceso,
                               cantidad_sesion = v.cantidad_sesion,
                               id_usuario = e.id_usuario,
                               email_usuario = e.email_usuario,
                               pass_usuario = e.pass_usuario,
                               nombre_usuario = e.nombre_usuario,
                               fono_usuario = e.fono_usuario,
                               cargo_usuario = e.cargo_usuario,
                               Rol_id_rol = e.Rol_id_rol,
                               idp = d.idp,
                               Proceso_id_proceso = d.Proceso_id_proceso,
                               Usuario_id_usuario = d.Usuario_id_usuario
                           }).Distinct().ToList();
                    lstcoachee = (from d in db.Proceso_usuario
                                  join v in db.Proceso
                                  on d.Proceso_id_proceso equals v.id_proceso
                                  join e in db.Usuario
                                  on d.Usuario_id_usuario equals e.id_usuario
                                  join em in db.Empresa
                                  on e.Empresa_id_empresa equals em.id_empresa
                                  orderby v.id_proceso ascending
                                  where (e.Rol_id_rol == 3)

                                  select new ProcesosTableViewModel
                                  {
                                      id_proceso_coachee = v.id_proceso,
                                      cargo_coachee = e.cargo_usuario,
                                      nombre_usuario_coachee = e.nombre_usuario,
                                      nombre_empresa_coachee = em.nombre_empresa

                                  }).Distinct().ToList();
                    lst.AddRange(lstcoachee);

                }

                return View("Index", lst);
            }
        }






    }
}