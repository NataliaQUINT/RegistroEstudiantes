using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEstudiantes.Modelos.Modelos
{
    public class Estudiante
    {
        public string? Id { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; } 
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email { get; set; }
        public int Edad { get; set; }
        public Curso? curso { get; set; }
        public bool? Estado { get; set; }
        public string NombreCompleto => $"{PrimerNombre} {ApellidoPaterno} {ApellidoMaterno}";
        public string EstadoTexto => Estado.HasValue ? (Estado.Value ? "Activo" : "Inactivo") : "Estado Desconocido";//operador ternario (if y else en una sola linea)
    }
}
