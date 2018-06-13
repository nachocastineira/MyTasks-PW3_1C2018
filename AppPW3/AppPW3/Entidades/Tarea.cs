namespace AppPW3.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tarea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tarea()
        {
            this.ArchivoTarea = new HashSet<ArchivoTarea>();
            this.ComentarioTarea = new HashSet<ComentarioTarea>();
        }
    
        public int IdTarea { get; set; }
        public int IdCarpeta { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> EstimadoHoras { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public short Prioridad { get; set; }
        public short Completada { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArchivoTarea> ArchivoTarea { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComentarioTarea> ComentarioTarea { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
