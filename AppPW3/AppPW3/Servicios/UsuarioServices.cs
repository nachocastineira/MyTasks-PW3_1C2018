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

        public void ActivarUsuario(Usuario usuario)
        {
            usuario.Activo = 1;
        }

        public Boolean VerificarMailExistente(Usuario usuario)
        {
            String emailUsuario = usuario.Email;
            var result = bdTareas.Usuario.Where(u => u.Email == emailUsuario).ToList();
            if (result.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}