using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class Riesgo
    {
        public Riesgo()
        {
            CategoriasRiesgo = new HashSet<CategoriasRiesgo>();
        }
        [Key]
        public int IdRiesgo { get; set; }
        [Required(ErrorMessage = "Debe introducir Descripción")]
        [Display(Name = "Riesgo:")]
        public string Descripcion { get; set; }

        public ICollection<CategoriasRiesgo> CategoriasRiesgo { get; set; }
    }
}
