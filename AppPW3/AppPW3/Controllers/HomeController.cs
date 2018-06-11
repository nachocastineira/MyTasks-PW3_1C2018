using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppPW3.Entidades;
using AppPW3.Servicios;

namespace AppPW3.Controllers
{
    public class HomeController : Controller
    {
        UsuarioServices usuarioServices = new UsuarioServices();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerificarLogin(Usuario usuario)
        {
            bool loginCorrecto = usuarioServices.VerificarLogin(usuario);

            if (loginCorrecto) //si el verificar login es true lo redirige a su index, sino lo lleva al login de nuevo
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult Logout()
        {
            return View();
        }

        public ActionResult Registracion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registracion(Usuario usuario)
        {
            //hay que terminarlo, y agregar mas condiciones por si el usuario ya existe que lo redirija a otro lado

            usuarioServices.RegistrarUsuario(usuario);

            return RedirectToAction("Login");
        }
    }
}