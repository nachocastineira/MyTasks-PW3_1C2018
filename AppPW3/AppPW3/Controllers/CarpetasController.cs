using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using AppPW3.Entidades;
using AppPW3.Servicios;

namespace AppPW3.Controllers
{
    public class CarpetasController : Controller
    {
        CarpetasServices carpetaServices = new CarpetasServices();
        TareasServices tareasServices = new TareasServices();
        UsuarioServices usuarioServices = new UsuarioServices();

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["idUsuario"]);
            if (Session["usuarioLogueado"] == null) //Si la variable de session que guarde en usuarioService es null lo mando al login
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

            return View(carpetaServices.ListarCarpetasPorUsuario(id)); //Andando User Logueado con SESSION
        }

        public ActionResult Crear()
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Carpeta carpeta)
        {
            //if (!ModelState.IsValid)
            //{
            //return View(carpeta);
            //}
            //else
            //{
                int id = Convert.ToInt32(Session["idUsuario"]);
                carpetaServices.CrearCarpeta(carpeta,id);
                return RedirectToAction("Index");
            //}

        }

        [HttpPost]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
               
               carpetaServices.EliminarCarpeta(id);
            }
            
            return RedirectToAction("Index");
        }
    }
}