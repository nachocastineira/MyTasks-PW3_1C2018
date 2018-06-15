using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppPW3.Entidades;
using AppPW3.Servicios;
using CaptchaMvc.HtmlHelpers;

namespace AppPW3.Controllers
{
    public class HomeController : Controller
    {
        UsuarioServices usuarioServices = new UsuarioServices();
        CarpetasServices carpetaServices = new CarpetasServices();
        TareasServices tareasServices = new TareasServices();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
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
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registracion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registracion(Usuario usuario)
        {
            //si la validacion del modelo es invalida, te manda de nuevo al registro
            if (ModelState.IsValid) {
                if (this.IsCaptchaValid("Validate your captcha"))
                {
                    usuarioServices.RegistrarUsuario(usuario);
                    Login(usuario);
                    return RedirectToAction("Index");
                }
                ViewBag.ErrorCaptcha = "Captcha incorrecto";
                return RedirectToAction("Registracion");
            }
            else { 
            return RedirectToAction("Registracion");
            }
        }
    }
}