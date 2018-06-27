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

        public List<Usuario> ListarUsuarios()
        {
            return bdTareas.Usuario.ToList();
        }

        public Usuario ObtenerUsuario(int id)
        {
            return bdTareas.Usuario.FirstOrDefault(u => u.IdUsuario == id);
        }

        public bool VerificarLogin(Usuario usuario)
        {
            foreach (Usuario usuariosRegistrados in ListarUsuarios())
            {
                if (usuario.Email.Equals(usuariosRegistrados.Email) && usuario.Contrasenia.Equals(usuariosRegistrados.Contrasenia))
                {
                    Session["usuarioLogueado"] = usuariosRegistrados; //guardo la variable de sesión del usuario logueado
                    Session["idUsuario"] = usuariosRegistrados.IdUsuario;
                    return true;
                }
            }
            return false;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            usuario.CodigoActivacion = "123123123123"; //hay que ver como generar el codigo de activacion
            usuario.Activo = 1; //por default un usuario se crea en estado activo
            usuario.FechaRegistracion = DateTime.Today;
            usuario.FechaActivacion = DateTime.Today;
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

        public void ActivarUsuario(Usuario usuario)
        {
            usuario.Activo = 1;
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

        //busca un usuario inactivo con el mismo mail, lo activa y lo modifica.
        public void ModificarUsuarioActivo(Usuario usuario) {
            var usuarioBuscado = bdTareas.Usuario.Where(u => u.Email == usuario.Email && u.Activo == 0).FirstOrDefault();
            usuarioBuscado.Activo = 1; 
            usuarioBuscado.Nombre = usuario.Nombre;
            usuarioBuscado.Apellido = usuario.Apellido;
            usuarioBuscado.Email = usuario.Email;
            bdTareas.SaveChanges();
         }
     }    
}