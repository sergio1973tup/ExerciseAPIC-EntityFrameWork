using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmpleadosAPI.Models
{
    public class modelDbContext: DbContext
    {
        public modelDbContext()

           : base("DefaultConnection")

        {

        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Pais> Paises { get; set; }
    }
}