using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppPW3.Entidades;

namespace AppPW3.Servicios
{
    public class CarpetasServices
    {
        TareasEntities bdTareas = new TareasEntities();

        public List<Carpeta> ListarCarpetas()
        {
            return bdTareas.Carpeta.ToList();
        }

        public Carpeta ObtenerCarpeta(int id)
        {
            return bdTareas.Carpeta.FirstOrDefault(c => c.IdCarpeta == id);
        }

        public void CrearCarpeta(Carpeta carpeta)
        {
            Usuario usuario = new Usuario();        //creo el user que va a crear la carpeta

            Carpeta nuevaCarpeta = new Carpeta();   

            //Hay que terminarlo, la carpeta no se crea porque no tiene ningun usuario asignado logueado

            nuevaCarpeta.Nombre = carpeta.Nombre;
            nuevaCarpeta.Descripcion = carpeta.Descripcion;
            nuevaCarpeta.FechaCreacion = DateTime.Now;

           // usuario.Carpeta.Add(carpeta);   //al usuario le agrego la carpeta que creó, esto es para 1 a N

            bdTareas.Carpeta.Add(carpeta);
        }

        public void ModificarCarpeta(Carpeta carpeta)
        {
            Carpeta carpetaActual = bdTareas.Carpeta.FirstOrDefault(c => c.IdCarpeta == carpeta.IdCarpeta);
            carpetaActual.Nombre = carpeta.Nombre;
            carpetaActual.Descripcion = carpeta.Descripcion;

            bdTareas.SaveChanges();
        }

        public void EliminarCarpeta(int id)
        {
            var carpetas = bdTareas.Carpeta;

            foreach (Carpeta c in carpetas)
            {
                bdTareas.Carpeta.Remove(c);
            }

            bdTareas.SaveChanges();
        }
    }
}