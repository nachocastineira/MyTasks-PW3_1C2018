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
            if (Session["usuarioLogueado"] == null) //Si la variable de session que guarde en usuarioService es null lo mando al login
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }
            return View(carpetaServices.ListarCarpetasPorUsuario(2)); //en lugar del 2 va el session del user logueado
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
                carpetaServices.CrearCarpeta(carpeta);
                return RedirectToAction("Index");
            //}

        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            carpetaServices.EliminarCarpeta(id);

            return RedirectToAction("Index");
        }
    }
}