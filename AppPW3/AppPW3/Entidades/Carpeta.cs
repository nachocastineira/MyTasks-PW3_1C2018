namespace AppPW3.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Carpeta
    {
        public int IdCarpeta { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^([^<>]){1,50}$", ErrorMessage = "Este campo {0} es obligatorio y tiene un máximo de 50 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [RegularExpression(@"^([^<>]){1,200}$", ErrorMessage = "Este campo {0} tiene un máximo de 200 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public System.DateTime FechaCreacion { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
