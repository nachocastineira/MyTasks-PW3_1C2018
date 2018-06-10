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
        TareasServices tareasServices = new TareasServices();

        
        public ActionResult Index()
        {
            return View(tareasServices.ListarTareas());
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Tarea tarea)
        {
            tareasServices.CrearTarea(tarea);

            return RedirectToAction("Index");
        }

        public ActionResult Detalle()
        {
            return View();
        }
    }
}