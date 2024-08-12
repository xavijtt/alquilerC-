using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquiler.Entidades
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string NombrePrimero { get; set; }
        public string NombreSegundo { get; set; }
        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Alias { get; set; }

        public string CI { get; set; }

        public string Ciudad { get; set; }

        public string Telefono { get; set; }

        public bool Estado { get; set; }
    }
}
