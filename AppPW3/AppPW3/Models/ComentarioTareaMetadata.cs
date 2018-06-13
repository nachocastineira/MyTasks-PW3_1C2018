using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPW3.Entidades
{
    internal class ComentarioTareaMetadata
    {
        public int IdComentarioTarea { get; set; }

        [Display(Name = "Comentario de la tarea")]
        public string Texto { get; set; }
        public int IdTarea { get; set; }

        [Display(Name = "Fecha de creación")]
        public System.DateTime FechaCreacion { get; set; }

        public virtual Tarea Tarea { get; set; }
    }
}