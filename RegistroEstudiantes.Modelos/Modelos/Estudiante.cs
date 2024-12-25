using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEstudiantes.Modelos.Modelos
{
    public class Estudiante
    {
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public int Edad { get; set; }
        public Curso curso { get; set; }
        public string NombreCompleto => $"{PrimerNombre}  {SegundoNombre} {ApellidoPaterno} {ApellidoMaterno}";
    }
}
