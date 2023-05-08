using EmpleadosAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmpleadosAPI.Controllers
{
    public class PaisController : Controller
    {
        private modelDbContext Db = new modelDbContext();

        public async Task<ActionResult> GetAll()
        {
            var paises = await Db.Paises.OrderBy(x => x.Nombre).ToListAsync();

            return Json(paises, JsonRequestBehavior.AllowGet);
        }
    }
}