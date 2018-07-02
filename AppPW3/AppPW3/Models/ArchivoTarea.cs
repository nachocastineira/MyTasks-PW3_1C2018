using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPW3.Entidades
{
    [MetadataType(typeof(ArhivoTareaMetadata))]
    public partial class ArchivoTarea
    {
        public string NombreSignificativoImagen
        {
            get
            {
                //en caso de ambos null, devuelve "ApellidoNombre"
                return string.Format("{0}", this.RutaArchivo ?? "Apellido");
            }
        }

    }
}