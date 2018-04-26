using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPW3.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet] //Hay que especificar que este va a ser get
        public ActionResult Home()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult Borrar(Empleado emp)
        //{
        //    var empRepo = new EmpleadoRepository();
        //    empRepo.Borrar(emp);

        //    return RedirectToAction("Listar", "Empleados");

        //}
    }
}
