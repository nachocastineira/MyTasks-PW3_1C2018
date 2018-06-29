using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppPW3.Entidades;

namespace AppPW3.Servicios
{
    public class TareasServices
    {
        TareasEntities bdTareas = new TareasEntities();

        public List<Tarea> ListarTareas()
        {
            return bdTareas.Tarea.OrderBy(t => t.Nombre).ToList();  //muestro tareas por orden ascendente
        }

        public List<Tarea> ListarTareasPorCarpetasDelUsuario(int idCarpeta, int idUsuario)
        {
            return bdTareas.Tarea.Where(t => t.IdCarpeta == idCarpeta && t.IdUsuario == idUsuario).OrderBy(t => t.Nombre).ToList();
        }

        public Tarea ObtenerTarea (int? id)
        {
            return bdTareas.Tarea.FirstOrDefault(t => t.IdTarea == id);
        }

        public void CrearTarea (Tarea tarea, int id)
        {

            tarea.IdUsuario = id;                //La tarea creada se le asigna al usuario logueado
            tarea.FechaCreacion = DateTime.Now;  //Fecha y hora actual al momento de crearla
            tarea.EstimadoHoras = null;          //revisar los errores cuando completo ese campo en el form
            tarea.Completada = 0;
        
            bdTareas.Tarea.Add(tarea);
            bdTareas.SaveChanges();
        }

        public void ModificarTarea (Tarea tarea)
        {
            Tarea tareaActual = bdTareas.Tarea.FirstOrDefault(t => t.IdTarea == tarea.IdTarea);

            tareaActual.Nombre = tarea.Nombre;
            tareaActual.Descripcion = tarea.Descripcion;
            tareaActual.EstimadoHoras = tarea.EstimadoHoras;
            tareaActual.FechaFin = tarea.FechaFin;
            tareaActual.Prioridad = tarea.Prioridad;
            tareaActual.Completada = tarea.Completada;

            bdTareas.SaveChanges();
        }

        public void EliminarTarea(int? id)
        {
            Tarea miTarea = ObtenerTarea(id);
            var tareas = bdTareas.Tarea;

            foreach (Tarea t in tareas)
            {
                bdTareas.Tarea.Remove(miTarea);
            }

            bdTareas.SaveChanges();
        }
    }
}