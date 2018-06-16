using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class CategoriasRiesgo
    {
        public CategoriasRiesgo()
        {
            ProblemaRiesgo = new HashSet<ProblemaRiesgo>();
        }
        [Key]
        public int IdCategoriasRiesgo { get; set; }
        [Required(ErrorMessage = "Debe introducir Descripción")]
        [Display(Name = "Categoria:")]
        public string Descripcion { get; set; }
        public int? IdRiesgo { get; set; }

        public Riesgo IdRiesgoNavigation { get; set; }
        public ICollection<ProblemaRiesgo> ProblemaRiesgo { get; set; }
    }
}
