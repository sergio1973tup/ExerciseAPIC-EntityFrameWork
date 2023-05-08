using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmpleadosAPI.Models
{
    public class Empleado
    {
        [Required, Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNac { get; set; }

        [ForeignKey("PaisId")]
        public virtual Pais Pais { get; set; }
        public int PaisId { get; set; }


    }

}