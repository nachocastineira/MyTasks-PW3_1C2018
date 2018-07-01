using System;
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
            int id = Convert.ToInt32(Session["idUsuario"]);

            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

            carpetaServices.ListarCarpetasPorUsuario(id);
            tareasServices.ListarTareasNoCompletadasDelUsuario(id);

            return View();
        }

        public ActionResult IndexAlternativo()  //INDEX PARA USUARIOS NO LOGUEADOS
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
            if (usuarioServices.VerificarUsuarioRegistrado(usuario))
            {
                if (usuarioServices.VerificarUsuarioActivo(usuario))
                {
                    if (usuarioServices.VerificarContraseniaLogin(usuario))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ViewBag.ErrorLogin = "Verificar usuario y/o contraseña";
                    return View("IndexAlternativo");
                }
                ViewBag.ErrorLogin = "Usuario no activo";
                return View("IndexAlternativo");
            }
            ViewBag.ErrorLogin = "Verificar usuario y/o contraseña";
            return View("IndexAlternativo");
        }




        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("IndexAlternativo", "Home");
        }

        public ActionResult Registracion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registracion(Usuario usuario)
        {   //valido model
            if (ModelState.IsValid)
            {   //valido que las contraseñas sean iguales
                if (usuario.Contrasenia == usuario.ContraseniaConfirm)
                {   //valido captcha
                    if (this.IsCaptchaValid("Validate your captcha"))
                    {   //valido usuario existente
                        if (usuarioServices.VerificarMailExistente(usuario))
                        {       //valido que el usuario existente esté este activo
                            if (usuarioServices.VerificarMailExistenteInactivo(usuario))
                            {
                                usuarioServices.ModificarUsuarioActivo(usuario);
                                Login(usuario);
                                return RedirectToAction("index", "home");
                            }
                            else
                            {   //si el mail ya existe, vuelve a la vista y se muestra error
                                ViewBag.ErrorMailExistente = "el email ya se encuentra registrado";
                                return View(usuario);
                            }
                        }
                        else
                        {   //si el mail no existe, se registra el usuario y se loguea
                            usuarioServices.RegistrarUsuario(usuario);
                            Login(usuario);
                            return RedirectToAction("index");
                        }
                    }
                    else
                    {   //si el captcha es incorrecto, vuelve a la vista y se muestra error
                        ViewBag.ErrorCaptcha = "Captcha incorrecto";
                        return View(usuario);
                    }
                }
                else
                {   //si las contraseñas no coinciden, vuelve a la vista y muestra error
                    ViewBag.ErrorContraseniaConfirm = "Las contraseñas no coinciden";
                    return View(usuario);
                }
            }
            else
            { //si las validaciones del modelo son incorrectas vuelve a la vista mostrando los mensajes de error
                { //si las validaciones del modelo son incorrectas vuelve a la vista mostrando los mensajes de error
                    return View(usuario);
                }
            }
        }
    }
}
