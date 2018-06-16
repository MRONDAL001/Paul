using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class Persona
    {
        [Key]
        public int IdPersona { get; set; }
        [Required(ErrorMessage = "Debe introducir Nombre")]
        [Display(Name = "Nombre:")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe introducir Apellido")]
        [Display(Name = "Apellido:")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe introducir Cedula")]
        [Display(Name = "Cedula:")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "Debe introducir Dirección")]
        [Display(Name = "Dirección:")]
        public string Direccion { get; set; }
    }
}
