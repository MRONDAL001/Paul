using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class Tipo
    {
        public Tipo()
        {
            Activos = new HashSet<Activos>();
        }

        public int IdTipo { get; set; }
        [Required(ErrorMessage = "Debe introducir Nombre")]
        [Display(Name = "Descripción:")]
        public string Descripcion { get; set; }

        public ICollection<Activos> Activos { get; set; }
    }
}
