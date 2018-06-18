using System;
using System.Collections.Generic;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class Activos
    {
        public int IdActivos { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdTecnico { get; set; }
        public int? IdTipo { get; set; }
        public string Ubicacion { get; set; }
        public bool Critico { get; set; }

        public Tecnico IdTecnicoNavigation { get; set; }
        public Tipo IdTipoNavigation { get; set; }
    }
}
