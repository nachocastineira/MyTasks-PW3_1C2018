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
            return bdTareas.Carpeta.OrderBy(c => c.Nombre).ToList(); //muestra por orden ascendente
        }

        public List<Carpeta> ListarCarpetasPorUsuario(int id)
        {
            return bdTareas.Carpeta.Where(c => c.IdUsuario == id).OrderBy(c => c.Nombre).ToList(); //con el where filtro las carpetas por usuario
        }

        public Carpeta ObtenerCarpeta(int? id)
        {
            return bdTareas.Carpeta.FirstOrDefault(c => c.IdCarpeta == id);
        }

        public void CrearCarpeta(Carpeta carpeta,int id)
        {
            Carpeta nuevaCarpeta = new Carpeta();

            nuevaCarpeta.IdUsuario = id; //andando el ID del usuario logueado
            nuevaCarpeta.Nombre = carpeta.Nombre;
            nuevaCarpeta.Descripcion = carpeta.Descripcion;
            nuevaCarpeta.FechaCreacion = DateTime.Now;

            // usuario.Carpeta.Add(carpeta);   //al usuario le agrego la carpeta que creó, esto es para 1 a N

            bdTareas.Carpeta.Add(nuevaCarpeta);
            bdTareas.SaveChanges();
        }

        public void ModificarCarpeta(Carpeta carpeta)
        {
            Carpeta carpetaActual = bdTareas.Carpeta.FirstOrDefault(c => c.IdCarpeta == carpeta.IdCarpeta);
            carpetaActual.Nombre = carpeta.Nombre;
            carpetaActual.Descripcion = carpeta.Descripcion;

            bdTareas.SaveChanges();
        }

        public void EliminarCarpeta(int? id)
        {
            Carpeta miCarpeta = ObtenerCarpeta(id);
            var carpetas = bdTareas.Carpeta;
            
            foreach (Carpeta c in carpetas)
            {
                
                bdTareas.Carpeta.Remove(miCarpeta);
            }

            bdTareas.SaveChanges();
        }
    }
}