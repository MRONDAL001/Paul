using System;
using System.Collections.Generic;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class Probabilidad
    {
        public int IdProbabilidad { get; set; }
        public int? Cuantitativo { get; set; }
        public string Descripcion { get; set; }
        public int? IdCualitativo { get; set; }

        public Cualitativo IdCualitativoNavigation { get; set; }
    }
}
