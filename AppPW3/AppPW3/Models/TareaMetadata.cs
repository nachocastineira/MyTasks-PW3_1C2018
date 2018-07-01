using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPW3.Entidades
{
    internal class TareaMetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TareaMetadata()
        {
            this.ArchivoTarea = new HashSet<ArchivoTarea>();
            this.ComentarioTarea = new HashSet<ComentarioTarea>();
        }

        public int IdTarea { get; set; }

        [Display(Name = "Carpeta")]
        public int IdCarpeta { get; set; }

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "¡{0} No Válido! Tiene un máximo de 50 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [RegularExpression(@"^([^<>]){1,200}$", ErrorMessage = "¡{0} No Válido! Tiene un máximo de 200 caracteres.)")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "¡{0} No Válido! Número hasta dos decimales.")]
        [Display(Name = "Horas Estimadas")]
        public Nullable<decimal> EstimadoHoras { get; set; }

        [Display(Name = "Fecha de Finalización")]
        public Nullable<System.DateTime> FechaFin { get; set; }

        [Display(Name = "Prioridad")]
        public short Prioridad { get; set; }

        [Display(Name = "Estado de tarea")]
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