using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppPW3.Entidades;
using AppPW3.Servicios;

namespace AppPW3.Controllers
{
    public class CarpetasController : Controller
    {
        CarpetasServices carpetaServices = new CarpetasServices();

        public ActionResult Index()
        {
            return View(carpetaServices.ListarCarpetas());
        }

        public ActionResult Crear()
        {
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