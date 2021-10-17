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
    public class AmbCoachController : Controller
    {
        // GET: AmbCoach
        public ActionResult Index()
        {

            List<AmbCoachTableViewModel> lst = null;
            List<AmbCoachTableViewModel> lstcoachee = null;
            List<AmbCoachTableViewModel> lst2 = null;
            List<AmbCoachTableViewModel> lst3 = null;


            using (COACHING6Entities db = new COACHING6Entities())
            {
                lst = (from d in db.Proceso_usuario
                       join v in db.Proceso
                       on d.Proceso_id_proceso equals v.id_proceso
                       join e in db.Usuario
                       on d.Usuario_id_usuario equals e.id_usuario
                       orderby v.id_proceso ascending
                       where (e.Rol_id_rol == 2)

                       select new AmbCoachTableViewModel
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

                              select new AmbCoachTableViewModel
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
                        select new AmbCoachTableViewModel
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


        public ActionResult Buscar(string coachs, string empresas)
        {
            var iddetectada = AccessController.valorid;
            List<AmbCoachTableViewModel> lst = null;
            List<AmbCoachTableViewModel> lstcoachee = null;
            List<AmbCoachTableViewModel> lst2 = null;
            List<AmbCoachTableViewModel> lst3 = null;

            Scoachs = coachs;
            Sempresas = empresas;


            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lst2 = (from d in db2.Usuario
                        orderby d.nombre_usuario ascending
                        where d.Rol_id_rol == 2
                        select new AmbCoachTableViewModel
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

                           select new AmbCoachTableViewModel
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

                                  select new AmbCoachTableViewModel
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

                           select new AmbCoachTableViewModel
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

                                  select new AmbCoachTableViewModel
                                  {
                                      id_proceso_coachee = v.id_proceso,
                                      nombre_usuario_coachee = e.nombre_usuario,
                                      cargo_coachee = e.cargo_usuario,
                                      nombre_empresa_coachee = em.nombre_empresa

                                  }).Distinct().ToList();
                    lst.AddRange(lstcoachee);

                }

                return View("Index", lst);
            }
        }


        public string Detalle_sesiones(long id_proceso)
        {
            string res = "";

            List<AmbCoachTableViewModel> lst = null;
            using (COACHING6Entities db = new COACHING6Entities())
            {
                lst = (from d in db.Sesion
                       orderby d.fecha_sesion ascending
                       where d.Proceso_id_proceso == id_proceso
                       select new AmbCoachTableViewModel
                       {
                           fecha_sesion = d.fecha_sesion,
                           descripcion_sesion = d.descripcion_sesion,
                           estado_sesion = d.estado_sesion,
                           documento_sesion = d.documento_sesion
                       }).ToList();
                foreach (var a in lst)
                {
                    DateTime fecha_sesion = a.fecha_sesion;
                    string descripcion_sesion = a.descripcion_sesion;
                    string estado_sesion = a.estado_sesion;
                    string documento_sesion = a.documento_sesion;
                    res = res +
                    "<tr><td>" + fecha_sesion + "</td>"
                    + "<td>" + descripcion_sesion + "</td>"
                    + "<td>" + estado_sesion + "</td>"
                    + "<td>" + documento_sesion + "</td></tr>";
                }

                //lst.AddRange(lst2);
                return res;
            }
        }
        public string Detalle_sesionesInicio(long id_proceso)
        {
            string res = "";

            List<AmbCoachTableViewModel> lst = null;
            using (COACHING6Entities db = new COACHING6Entities())
            {
                lst = (from d in db.Sesion
                       orderby d.fecha_sesion ascending
                       where d.Proceso_id_proceso == id_proceso
                       select new AmbCoachTableViewModel
                       {
                           fecha_sesion = d.fecha_sesion,
                           descripcion_sesion = d.descripcion_sesion,
                           estado_sesion = d.estado_sesion,
                           documento_sesion = d.documento_sesion
                       }).ToList();
                foreach (var a in lst)
                {
                    DateTime fecha_sesion = a.fecha_sesion;
                    string descripcion_sesion = a.descripcion_sesion;
                    string estado_sesion = a.estado_sesion;
                    string documento_sesion = a.documento_sesion;
                    res = res +
                    "<tr><td>" + fecha_sesion + "</td>"
                    + "<td>" + descripcion_sesion + "</td>"
                    + "<td>" + estado_sesion + "</td>"
                    + "<td>" + documento_sesion + "</td></tr>";
                }

                //lst.AddRange(lst2);
                return res;
            }
        }


        //Procesos
        //Procesos

        public ActionResult NuevoProceso()
        {
            List<AmbCoachTableViewModel> lstcoach = null;
            List<AmbCoachTableViewModel> lstcoachee = null;
            using (COACHING6Entities db2 = new COACHING6Entities())
            {
                lstcoach = (from d in db2.Usuario
                        orderby d.nombre_usuario ascending
                        where d.Rol_id_rol == 2
                        select new AmbCoachTableViewModel
                        {
                            id_usuario = d.id_usuario,
                            nombre_usuario = d.nombre_usuario
                        }).ToList();

            }
            List<SelectListItem> items = lstcoach.ConvertAll(d =>
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
                lstcoachee = (from d in db2.Usuario
                        orderby d.nombre_usuario ascending
                        where d.Rol_id_rol == 3
                        select new AmbCoachTableViewModel
                        {
                            id_usuario = d.id_usuario,
                            nombre_usuario = d.nombre_usuario
                        }).ToList();

            }
            List<SelectListItem> items2 = lstcoachee.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre_usuario.ToString(),
                    Value = d.id_usuario.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items2 = items2;
            return View();
        }

        [HttpPost]
        public ActionResult NuevoProceso(NProcesosTableViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (COACHING6Entities db = new COACHING6Entities())
                    {
                        var oProceso = new Proceso();
                       // oProceso.id_proceso = model.id_proceso;
                        oProceso.fecha_inicio = model.fecha_inicio;
                        oProceso.indicador_exito= model.indicador_exito;
                        oProceso.plan_accion= model.plan_accion;
                        oProceso.objetivo_proceso= model.objetivo_proceso;
                        oProceso.cantidad_sesion= model.cantidad_sesion;
                        db.Proceso.Add(oProceso);
                        db.SaveChanges();

                        //PROCESO_USUARIO
                        List<AmbCoachTableViewModel> lst = null;
                        using (COACHING6Entities db2 = new COACHING6Entities())
                        {
                            lst = (from d in db2.Proceso
                                        orderby d.id_proceso ascending
                                        select new AmbCoachTableViewModel
                                        {
                                            id_proceso = d.id_proceso
                                        }).ToList();

                        }
                        var extProceso = lst.Last();
                        var oProceso_coach = new Proceso_usuario();
                        oProceso_coach.Proceso_id_proceso = extProceso.id_proceso;
                        oProceso_coach.Usuario_id_usuario = model.id_coach;
                        db.Proceso_usuario.Add(oProceso_coach);
                        db.SaveChanges();
                        var oProceso_coachee = new Proceso_usuario();
                        oProceso_coachee.Proceso_id_proceso = extProceso.id_proceso;
                        oProceso_coachee.Usuario_id_usuario = model.id_coachee;
                        db.Proceso_usuario.Add(oProceso_coachee);
                        db.SaveChanges();
                        
                    }
                    return Redirect("~/AmbCoach");
                }
                return View(model);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }


        //Procesos
        //Procesos

        public ActionResult Editar(int id_proceso)
        {
            NProcesosTableViewModel model = new NProcesosTableViewModel();
            using (COACHING6Entities db = new COACHING6Entities())
            {
                var oProceso = db.Proceso.Find(id_proceso);
                model.id_proceso = oProceso.id_proceso;
                model.fecha_inicio = oProceso.fecha_inicio;
                model.indicador_exito = oProceso.indicador_exito;
                model.plan_accion = oProceso.plan_accion;
                model.objetivo_proceso = oProceso.objetivo_proceso;
                model.cantidad_sesion = oProceso.cantidad_sesion;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Editar(NProcesosTableViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (COACHING6Entities db = new COACHING6Entities())
                    {
                        var oProceso = db.Proceso.Find(model.id_proceso);
                        oProceso.id_proceso = model.id_proceso;
                        oProceso.fecha_inicio = model.fecha_inicio;
                        oProceso.indicador_exito = model.indicador_exito;
                        oProceso.plan_accion = model.plan_accion;
                        oProceso.objetivo_proceso = model.objetivo_proceso;
                        oProceso.cantidad_sesion = model.cantidad_sesion;
                        db.Entry(oProceso).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/AmbCoach");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Eliminar(int id_proceso)
        {
            UsuariosTableViewModel model = new UsuariosTableViewModel();
            using (COACHING6Entities db = new COACHING6Entities())
            {
                List<AmbCoachTableViewModel> lst = null;
                using (COACHING6Entities db2 = new COACHING6Entities())
                {
                    lst = (from d in db2.Proceso_usuario
                           orderby d.Proceso_id_proceso ascending
                           where d.Proceso_id_proceso==id_proceso
                           select new AmbCoachTableViewModel
                           {
                               idp = d.idp,
                               Proceso_id_proceso = d.Proceso_id_proceso
                           }).ToList();

                }
                var extProceso1 = lst.First();
                var extProceso2 = lst.Last();
                var oProceso_usuario = db.Proceso_usuario.Find(extProceso1.idp);
                var oProceso_usuario2 = db.Proceso_usuario.Find(extProceso2.idp);
                db.Proceso_usuario.Remove(oProceso_usuario);
                db.Proceso_usuario.Remove(oProceso_usuario2);
                var oProceso = db.Proceso.Find(id_proceso);
                db.Proceso.Remove(oProceso);
                db.SaveChanges();
            }
            return Redirect("~/AmbCoach");
        }

    }
}