namespace AppPW3.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Carpeta
    {
        public int IdCarpeta { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaCreacion { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
