using System;
using System.Collections.Generic;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class Cualitativo
    {
        public Cualitativo()
        {
            Impacto = new HashSet<Impacto>();
            Probabilidad = new HashSet<Probabilidad>();
        }

        public int IdCualitativo { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Impacto> Impacto { get; set; }
        public ICollection<Probabilidad> Probabilidad { get; set; }
    }
}
