using Actividad4LengProg3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Actividad4LengProg3.Controllers
{
    public class RecintosController : Controller
    {


        public IActionResult Index()
        {
            return View(Recinto.Recintos);
        }

        public IActionResult Create()
        {

            var model = new RecintoViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecintoViewModel recinto)
        {
            if (ModelState.IsValid)
            {
                Recinto.Recintos.Add(recinto);
                return RedirectToAction(nameof(Index));
            }
            return View(recinto);
        }

        [HttpGet("Recintos/Details/{codigo}")]
        public ActionResult Details(int codigo)
        {
            var est = Recinto.GetByCodigo(codigo);
            if (est == null)
            {
                TempData["ErrorMessage"] = "Recinto no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            return View(est);
        }

        [HttpGet("Recintos/Edit/{codigo}")]
        public ActionResult Edit(int codigo)
        {
            var est = Recinto.GetByCodigo(codigo);
            if (est == null)
            {
                TempData["ErrorMessage"] = "Recinto no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            return View(est);
        }
        [HttpPost("Recintos/Edit/{codigo}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int codigo, RecintoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Revisa los campos marcados en rojo.";
                    return View(model);
                }
                Console.WriteLine("Llegue hasta aqui");
                var actual = Recinto.GetByCodigo(codigo);
                if (actual == null)
                {
                    TempData["ErrorMessage"] = "Recinto no encontrado.";
                    return RedirectToAction(nameof(Index));
                }
                actual.Nombre = model.Nombre;
                actual.Direccion = model.Direccion;

                TempData["SuccessMessage"] = "Cambios guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("Recintos/Delete/{codigo}")]
        public ActionResult Delete(int codigo)
        {
            var est = Recinto.GetByCodigo(codigo);
            if (est == null)
            {
                TempData["ErrorMessage"] = "Recinto no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            return View(est);
        }


        [HttpPost("Recintos/DeleteConfirmed/{codigo}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codigo)
        {
            try
            {
                var rec = Recinto.GetByCodigo(codigo);
                if (rec != null)
                {
                    Recinto.Recintos.Remove(rec);
                    TempData["SuccessMessage"] = "Recinto eliminado correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se encontró el Recinto.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocurrió un error al eliminar.";
                return RedirectToAction(nameof(Index));
            }
        }

    }

}



