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

            ViewBag.CarpetasDelUsuario = carpetaServices.ListarCarpetasPorUsuario(idUser); //Con este ViewBag mando las carpetas de ese usuario al form para crear tarea

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Tarea tarea)
        {
            int id = Convert.ToInt32(Session["idUsuario"]);
            ViewBag.CarpetasDelUsuario = carpetaServices.ListarCarpetasPorUsuario(id);

            //if (ModelState.IsValid)
            //{
                
                tareasServices.CrearTarea(tarea, id);

                return RedirectToAction("Index", "Carpetas");
            //}
            //else
            //{
            //    return View(tarea);
            //}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearComentario(ComentarioTarea comentario, int id)
        {
            int idUser = Convert.ToInt32(Session["idUsuario"]);
            comentario.IdTarea = id;
            tareasServices.CrearComentarioTarea(comentario, id);

            return RedirectToAction("Index", "Carpetas");
        }

        public ActionResult CrearComentario(int? id)

        {

            return View();
        }

        public ActionResult Detalle(int? id)
        
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

            ViewBag.ComentariosTarea = tareasServices.ListarComentariosPorTarea(id);

            return View(tareasServices.ObtenerTarea(id));
        }

        //[HttpPost]
        //public ActionResult Detalle(ComentarioTarea comentario, int idTarea)
        //{
        //    tareasServices.CrearComentarioTarea(comentario, idTarea);

        //    return RedirectToAction("Detalle", "Tareas");
        //}


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

        [HttpPost]
        public ActionResult CompletarTarea(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Carpetas");
            }
            else
            {
                tareasServices.CompletarTarea(id);
            }

            return RedirectToAction("Index", "Carpetas");
        }

        

    }
}