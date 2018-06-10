namespace AppPW3.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
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

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "Este campo {0} es obligatorio y tiene un máximo de 50 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [RegularExpression(@"^([^<>]){1,200}$", ErrorMessage = "Este campo {0} tiene un máximo de 200 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [RegularExpression(@"[0-9]{0,}\.[0-9]{2}", ErrorMessage = "Número con hasta 2 decimales")]
        [Display(Name = "Horas Estimadas")]
        public Nullable<decimal> EstimadoHoras { get; set; }

        [Display(Name = "Fecha de Finalización")]
        public Nullable<System.DateTime> FechaFin { get; set; }

        [Display(Name = "Prioridad")]
        public short Prioridad { get; set; }

        [Display(Name = "Estado")]
        public short Completada { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime FechaCreacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArchivoTarea> ArchivoTarea { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComentarioTarea> ComentarioTarea { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
