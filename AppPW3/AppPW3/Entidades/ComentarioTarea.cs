namespace AppPW3.Entidades
{
    using System;
    using System.Collections.Generic;

    public partial class ComentarioTarea
    {
        public int IdComentarioTarea { get; set; }
        public string Texto { get; set; }
        public int IdTarea { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    
        public virtual Tarea Tarea { get; set; }
    }
}
