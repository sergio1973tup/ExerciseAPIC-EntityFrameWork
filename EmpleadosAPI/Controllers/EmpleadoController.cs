using EmpleadosAPI.DTO;
using EmpleadosAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmpleadosAPI.Controllers
{
    public class EmpleadoController : Controller
    {
        private modelDbContext Db = new modelDbContext();

        [HttpGet]
        public async Task<ActionResult> GetAll(string filtro)
        {
            var empleadosFiltrados = await Db.Empleados.Where(x => x.Nombre.Contains(filtro) || x.Pais.Nombre.Contains(filtro)).ToListAsync();
            var empleados = empleadosFiltrados;
            var empleadosDTO = empleados.Select(e => new EmpleadoGrillaDTO
            {
                Id = e.ID,
                Nombre = e.Nombre,
                paisNombre = e.Pais.Nombre, 
                FechaNac = e.FechaNac.ToString("yyyy-MM-dd"),
            });

            return Json(empleadosDTO, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> Editar(EmpleadoGrillaDTO empleadoDTO)
        {
            var errores = controlErrores(empleadoDTO);

            if (errores.Count == 0)
            {
                var empleadoDb = await Db.Empleados.FindAsync(empleadoDTO.Id);

                DateTime fechaformateada = DateTime.ParseExact(empleadoDTO.FechaNac, "yyyy-MM-dd", null);

                empleadoDb.Nombre = empleadoDTO.Nombre;
                empleadoDb.PaisId = empleadoDTO.pais;
                empleadoDb.FechaNac = fechaformateada;

                await Db.SaveChangesAsync();
            }

            return Json(errores);

        }

        [HttpPost]
        public async Task<ActionResult> Crear(EmpleadoGrillaDTO EmpleadoDTO)
        {
            var errores = this.controlErrores(EmpleadoDTO);

            if (errores.Count == 0)
            {
                DateTime fechaFormateada = DateTime.ParseExact(EmpleadoDTO.FechaNac, "yyyy-MM-dd",
                    CultureInfo.CurrentCulture);

                Empleado NewEmpleado = new Empleado();
                NewEmpleado.Nombre = EmpleadoDTO.Nombre;
                NewEmpleado.PaisId = EmpleadoDTO.pais;
                NewEmpleado.FechaNac = fechaFormateada;


                Db.Empleados.Add(NewEmpleado);
                await Db.SaveChangesAsync();
            }

            return Json(errores);
            
        }

        [HttpGet]
        public async Task<ActionResult> Get(int ID)
        {
            Empleado empleadoEditar = await Db.Empleados.FirstAsync(x => x.ID == ID);
            Pais paisEditar = await Db.Paises.FindAsync(empleadoEditar.PaisId);

            var empleadoDTO = new EmpleadoGrillaDTO();

            empleadoDTO.Id = empleadoEditar.ID;
            empleadoDTO.Nombre = empleadoEditar.Nombre;
            empleadoDTO.pais = paisEditar.ID;
            empleadoDTO.paisNombre = paisEditar.Nombre;
            empleadoDTO.FechaNac = empleadoEditar.FechaNac.ToString("yyyy-MM-dd");


            return Json(empleadoDTO, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Borrar(int ID)
        {
            Empleado empleadoBorrar = await Db.Empleados.FindAsync(ID);

            if (empleadoBorrar != null)
            {
                Db.Empleados.Remove(empleadoBorrar);
                await Db.SaveChangesAsync();

                return Json("empleado borrado", JsonRequestBehavior.AllowGet);
            }
            else 
            {
                return Json("empleado no borrado", JsonRequestBehavior.AllowGet);
            }

        }

        public List<string> controlErrores(EmpleadoGrillaDTO empleadoDTO)
        {
            var errores = new List<string>();

            if (string.IsNullOrEmpty(empleadoDTO.Nombre))
            {
                errores.Add("Completar nombre");
            }
            if (empleadoDTO.pais == 0)
            {
                errores.Add("Completar Pais");
            }
            if (empleadoDTO.FechaNac == "")
            {
                errores.Add("Completar fecha de nacimiento");
            }

            return errores;
        }
    }
}