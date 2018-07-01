using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPW3.Entidades
{
    internal class ArhivoTareaMetadata
    {

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Ruta")]
        public string RutaArchivo { get; set; }

        public int IdArchivoTarea { get; set; }
        public int IdTarea { get; set; }
        public System.DateTime FechaCreacion { get; set; }
    }
}