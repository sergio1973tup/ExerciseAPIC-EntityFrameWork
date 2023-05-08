using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpleadosAPI.DTO
{
    public class EmpleadoGrillaDTO
    {
        public virtual int Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual int pais { get; set; }
        public virtual string paisNombre { get; set; }
        public virtual string FechaNac { get; set; }

    }
}