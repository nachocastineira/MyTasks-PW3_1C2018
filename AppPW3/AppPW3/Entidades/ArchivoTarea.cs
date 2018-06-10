namespace AppPW3.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArchivoTarea
    {
        public int IdArchivoTarea { get; set; }
        public string RutaArchivo { get; set; }
        public int IdTarea { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    
        public virtual Tarea Tarea { get; set; }
    }
}
