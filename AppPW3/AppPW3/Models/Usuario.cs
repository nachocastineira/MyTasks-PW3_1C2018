using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPW3.Entidades
{
    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario
    {   //atributo usado en el logueo, no persiste en la base de datos.
        public string ContraseniaConfirm { get; set; }
    }
}