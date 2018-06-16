using System;
using System.Collections.Generic;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class ProblemaRiesgo
    {
        public int IdProblemaRiesgo { get; set; }
        public string Descripcion { get; set; }
        public int? IdCategoriaRiesgo { get; set; }

        public CategoriasRiesgo IdCategoriaRiesgoNavigation { get; set; }
    }
}
