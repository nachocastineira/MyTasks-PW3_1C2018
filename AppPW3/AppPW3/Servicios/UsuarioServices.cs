using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppPW3.Entidades;
using System.Web.SessionState;

namespace AppPW3.Servicios
{
    public class UsuarioServices : System.Web.UI.Page
    {
        TareasEntities bdTareas = new TareasEntities();
        CarpetasServices carpetasServices = new CarpetasServices();

        public List<Usuario> ListarUsuarios()
        {
            return bdTareas.Usuario.ToList();
        }

        public Usuario ObtenerUsuario(int id)
        {
            return bdTareas.Usuario.FirstOrDefault(u => u.IdUsuario == id);
        }

        //verifico que el usuario esté registrado en el sistema
        public bool VerificarUsuarioRegistrado(Usuario usuario) {
            var UsuarioRegistrado = bdTareas.Usuario.Where(u => u.Email == usuario.Email).FirstOrDefault();
            if (UsuarioRegistrado != null) {
                return true;
            }
            return false;
        }

        //verifico que el usuario esté activo
        public bool VerificarUsuarioActivo(Usuario usuario)
        {//traigo un usuario cuyo mail coincida con el ingresado y chequeo que el estado sea activo. 
            Usuario usuarioActivo = bdTareas.Usuario.Where(u => u.Email == usuario.Email).FirstOrDefault();
                if (usuarioActivo.Activo == 1)
                {
                    return true;
                }
                return false;
        }

        //verifico que la contraseña ingresada sea correcta
        public bool VerificarContraseniaLogin(Usuario usuario)
        {
                Usuario usuarioContraseniasCoincidentes = bdTareas.Usuario.Where(u => u.Contrasenia == usuario.Contrasenia && u.Email == usuario.Email).FirstOrDefault();
                if (usuarioContraseniasCoincidentes != null)
                {
                    Session["usuarioLogueado"] = usuarioContraseniasCoincidentes; //guardo la variable de sesión del usuario logueado
                    Session["idUsuario"] = usuarioContraseniasCoincidentes.IdUsuario;
                    return true;
                }
                return false;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            usuario.CodigoActivacion = "4AE52B1C-C8E2-4AB1-8EFD-119FCB87F5B3"; 
            usuario.Activo = 1; //por default un usuario se crea en estado activo
            usuario.FechaRegistracion = DateTime.Today;
            usuario.FechaActivacion = DateTime.Today;

            usuario.Carpeta.Add(new Carpeta { Nombre = "General", Descripcion = "Bienvenido, esta será su carpeta por defecto.", FechaCreacion = DateTime.Now }); //Carpeta por defecto para un nuevo usuario

            bdTareas.Usuario.Add(usuario);
            bdTareas.SaveChanges();
        }

        //verifica si las contraseñas son iguales. Si son iguales da true, si no coinciden da false
        public Boolean VerificarContraseñasIguales(Usuario usuario) {
            if(usuario.Contrasenia == usuario.ContraseniaConfirm) { 
            return true;
            }
            return false;
        }


        //busca si ya existe el mail del usuario. Si existe da true, si no existe da false
        public Boolean VerificarMailExistente(Usuario usuario)
        {
            Usuario usuarioBuscado = bdTareas.Usuario.Where(u => u.Email == usuario.Email).FirstOrDefault();
            if (usuarioBuscado != null)
            {
                return true;
            }
            return false;
        }

        //verifica que ya exista el mail y que ademas esté inactivo
        public Boolean VerificarMailExistenteInactivo(Usuario usuario)
        {
            var usuarioBuscado = bdTareas.Usuario
                .Where(u => u.Email == usuario.Email 
                && usuario.Activo==0)
                .FirstOrDefault();
            if (usuarioBuscado != null)
            {
                return true;
            }
            return false;
        }

        //busca un usuario inactivo con el mismo mail, lo activa y lo modifica. Solo actualiza Nombre, Apellido y Contraseña, se agrega fecha de activacion y el Activo cambia a 1
        public void ModificarUsuarioActivo(Usuario usuario) {
            var usuarioBuscado = bdTareas.Usuario.Where(u => u.Email == usuario.Email && u.Activo == 0).FirstOrDefault();
            usuarioBuscado.Activo = 1; 
            usuarioBuscado.Nombre = usuario.Nombre;
            usuarioBuscado.Apellido = usuario.Apellido;
            usuarioBuscado.Contrasenia = usuario.Contrasenia;
            usuarioBuscado.ContraseniaConfirm = usuario.ContraseniaConfirm;
            usuarioBuscado.FechaActivacion = DateTime.Now;
            bdTareas.SaveChanges();
         }

        public void CrearCookie(Usuario usuario)
        {
            HttpCookie cookie = new HttpCookie("User");
            cookie["ID"] = usuario.IdUsuario.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public Usuario TraerCookie()
        {
            try
            {
                HttpCookie Cookie = HttpContext.Current.Request.Cookies.Get("User");
                String id = Cookie["ID"];

                if (id != null)
                {
                    if (id != String.Empty)
                    {
                        int idUsuario = int.Parse(id);

                        Usuario usuario = ObtenerUsuario(idUsuario);

                        if (usuario != null) return usuario;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
    }
}
