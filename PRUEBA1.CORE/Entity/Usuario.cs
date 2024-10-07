using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.CORE.Entity
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; } = string.Empty;
        public DateTime? FechaEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; } = string.Empty;
        public string IpLocal { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public int IdRol { get; set; }
    }
}
