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
            if (Session["usuarioLogueado"] == null) //Si la variable de session que guarde en usuarioService es null no mando al login
            {
                return RedirectToAction("Login", "Home");
            }

            return View(carpetaServices.ListarCarpetas());
        }

        public ActionResult Crear()
        {
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Crear(Carpeta carpeta)
        {
            carpetaServices.CrearCarpeta(carpeta);

            return RedirectToAction("Index");
        }
    }
}