using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class Problema
    {
        [Key]
        public int IdProblema { get; set; }
        [Required(ErrorMessage = "Debe introducir Descripción")]
        [Display(Name = "Descripción:")]
        public string Descripcion { get; set; }
        public int? Estado { get; set; }
    }
}
