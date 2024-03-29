﻿using AdopcionAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTO
{
    public class CentroConsultaConMascotasDTO
    {
        public int Id { get; set; }
        [Required] //el campo sera obligatorio con esta notificacion
        [StringLength(150)]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public List<Mascota> Mascotas { get; set; }
    }
}
