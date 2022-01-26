using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopcionAPI.Models
{
    public class UsuarioDTO
    {

        public int Id { get; set; }
        [Required] //el campo sera obligatorio con esta notificacion
        [StringLength(150)]
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public string Contraseña { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
