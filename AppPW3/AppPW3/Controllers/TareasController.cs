using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppPW3.Entidades;
using AppPW3.Servicios;
using AppPW3.Utilities;

namespace AppPW3.Controllers
{
    public class TareasController : Controller
    {
        CarpetasServices carpetaServices = new CarpetasServices();
        TareasServices tareasServices = new TareasServices();
        UsuarioServices usuarioServices = new UsuarioServices();


        public ActionResult Index()
        {
            int idUser = Convert.ToInt32(Session["idUsuario"]);
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }
            return View(tareasServices.ListarTareasPorUsuario(idUser));  //parametro 1 idCARPETA, par2 idUsuario
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearComentario(ComentarioTarea comentario, int? id)
        {
            int idTarea = Convert.ToInt32(id);
            int idUser = Convert.ToInt32(Session["idUsuario"]);
            comentario.IdTarea = idTarea;
            tareasServices.CrearComentarioTarea(comentario, idTarea);

            return RedirectToAction("Detalle", "Tareas", new { id = idTarea });
        }

        public ActionResult CrearComentario(int? id)
        {
            int idUser = Convert.ToInt32(Session["idUsuario"]);
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }


            int idTarea = Convert.ToInt32(id);
            ViewBag.idTarea = idTarea;

            return View();
        }

        public ActionResult Detalle(int? id)

        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

            ViewBag.ArchivosTarea = tareasServices.ListarArchivosPorTarea(id);
            ViewBag.ComentariosTarea = tareasServices.ListarComentariosPorTarea(id);

            return View(tareasServices.ObtenerTarea(id));
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

        //metodo que recibe datos desde el boton "completar" desde el home, completa la tarea y vuelve al home
        public ActionResult CompletarTareaHome(int? id)
        {
            tareasServices.CompletarTarea(id);
            return RedirectToAction("Index", "Home");
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

        public ActionResult AdjuntarArchivo(int? id)
        {
            int idUser = Convert.ToInt32(Session["idUsuario"]);
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

            int idTarea = Convert.ToInt32(id);
            ViewBag.idTarea = idTarea;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdjuntarArchivo(ArchivoTarea archivo, int? id)
        {
            int idTarea = Convert.ToInt32(id);
            int idUser = Convert.ToInt32(Session["idUsuario"]);
            Tarea miTarea = tareasServices.ObtenerTarea(idTarea);
            String stringTarea = miTarea.Nombre;
            archivo.IdTarea = idTarea;
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string nombreSignificativo = archivo.NombreSignificativoImagen;
                

                string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo, idTarea);
                archivo.RutaArchivo = pathRelativoImagen;
            }

            tareasServices.CrearArchivoTarea(archivo);

            return RedirectToAction("Detalle", "Tareas", new { id = idTarea });
        }

    }
}