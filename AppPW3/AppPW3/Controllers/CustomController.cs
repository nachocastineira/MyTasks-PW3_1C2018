using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPW3.Controllers
{
    public class CustomController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /*Si se entra en algún ActionResult del Home, no hago nada. Los métodos de ese 
             controller tienen diferentes validaciones. Si no se cumple, entonces si generalizo 
             con lo de abajo.*/

            string actionName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (actionName.Equals("Home")) return;

            //Si no tenes permiso te mando al login con la url como parametro
            if (Session["idUsuario"] == null)
            {
                string urlIntentada = Request.Url.ToString();
                UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
                string urlNueva = u.Action("IndexAlternativo", "Home", new { ReturnUrl = urlIntentada });
                filterContext.Result = Redirect(urlNueva);
            }
        }
    }
}