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
            Carpeta nuevaCarpeta = new Carpeta();

            nuevaCarpeta.Nombre = carpeta.Nombre;
            nuevaCarpeta.Descripcion = carpeta.Descripcion;

            bdTareas.Carpeta.Add(carpeta);
        }

        public void ModificarCarpeta(Carpeta carpeta)
        {
            Carpeta carpetaActual = ObtenerCarpeta(carpeta.IdCarpeta);
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