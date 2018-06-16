using System;
using System.Collections.Generic;

namespace SmartAdmin.Web.Models.Sistema
{
    public partial class Tecnico
    {
        public int IdTecnico { get; set; }
        public int IdPersona { get; set; }
        public int Estado { get; set; }

        public Persona IdPersonaNavigation { get; set; }
    }
}
