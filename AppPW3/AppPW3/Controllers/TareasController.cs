using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppPW3.Entidades;
using AppPW3.Servicios;

namespace AppPW3.Controllers
{
    public class TareasController : Controller
    {
        CarpetasServices carpetaServices = new CarpetasServices();
        TareasServices tareasServices = new TareasServices();
        UsuarioServices usuarioServices = new UsuarioServices();


        public ActionResult Index(int id) 
        {
            int idUser = Convert.ToInt32(Session["idUsuario"]);
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

            return View(tareasServices.ListarTareasPorCarpetasDelUsuario(id, idUser));  //parametro 1 idCARPETA, par2 idUsuario
        }

        public ActionResult Crear()
        {
            int idUser = Convert.ToInt32(Session["idUsuario"]);
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

            return View(carpetaServices.ListarCarpetasPorUsuario(idUser));
        }

        [HttpPost]
        public ActionResult Crear(Tarea tarea)
        {
            int id = Convert.ToInt32(Session["idUsuario"]);
            tareasServices.CrearTarea(tarea,id);

            return RedirectToAction("Index");
        }

        public ActionResult Detalle()
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

            return View();
        }


        [HttpPost]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Carpetas");
            }
            else
            {

                tareasServices.EliminarTarea(id);
            }

            return RedirectToAction("Index", "Carpetas");
        }
    }
}