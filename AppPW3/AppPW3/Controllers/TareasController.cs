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
        //[ValidateAntiForgeryToken]
        public ActionResult Crear(Tarea tarea)
        {
            int id = Convert.ToInt32(Session["idUsuario"]);

            if (ModelState.IsValid)
            {
                tareasServices.CrearTarea(tarea, id);

                return RedirectToAction("Index", "Tareas");
            }
            else
            {
                return View(tarea);
            }
        }

        public ActionResult Detalle(int? idTarea)
        //public ActionResult Detalle(int idTarea) //La idea aca es cargar los datos se esa tarea seleccionada, no se por que da error
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

            Tarea tareaSeleccionada = tareasServices.ObtenerTarea(idTarea);           //aca obtendria la tarea segun el id del parametro, y la retorno mas abajo
            ViewBag.comentarios = tareasServices.ListarComentariosPorTarea(idTarea);  //con un viebag mando los comentarios desde el controller a la vista

            return View(tareaSeleccionada); //Traigo la tarea seleccionada, y cargo los valores que tenga
            //return View(); //Traigo la tarea seleccionada, y cargo los valores que tenga
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

            return RedirectToAction("Index", "Tareas");
        }
    }
}