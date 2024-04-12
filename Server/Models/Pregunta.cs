using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Pregunta
    {
        public int Id { get; set; }
        public string _Pregunta { get; set; } = "";
        public int Respuesta { get; set; } // Maximo 5 
    }
}
