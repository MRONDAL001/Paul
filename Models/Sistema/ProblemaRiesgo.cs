using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class ProblemaRiesgo
    {
        public int IdProblemaRiesgo { get; set; }
        [Required(ErrorMessage = "Debe introducir problema del riesgo")]
        [Display(Name = "Problema del riesgo:")]
        public string Descripcion { get; set; }
        public int? IdCategoriaRiesgo { get; set; }

        public CategoriasRiesgo IdCategoriaRiesgoNavigation { get; set; }
    }
}
