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
            if (Session["usuarioLogueado"] == null)
            {
                return RedirectToAction("IndexAlternativo", "Home");
            }

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
            bool loginCorrecto = usuarioServices.VerificarLogin(usuario);

            if (loginCorrecto) //si el verificar login es true lo redirige a su index, sino lo lleva al login de nuevo
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("IndexAlternativo");
            }
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
                                return RedirectToAction("index");
                            }
                            else
                            {
                                ViewBag.ErrorMailExistente = "el email ya se encuentra registrado";
                                return View(usuario);
                            }
                        }
                        else
                        {
                            usuarioServices.RegistrarUsuario(usuario);
                            Login(usuario);
                            return RedirectToAction("index");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorCaptcha = "Captcha incorrecto";
                        return View(usuario);
                    }
                }
                else
                {
                    ViewBag.ErrorContraseniaConfirm = "Las contraseñas no coinciden";
                    return View(usuario);
                }
            }
            else
            {
                return View(usuario);
            }



        }

    }
}    
