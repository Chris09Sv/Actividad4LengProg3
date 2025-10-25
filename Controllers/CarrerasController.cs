using Actividad4LengProg3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Actividad4LengProg3.Controllers
{
    public class CarrerasController : Controller
    {
        // GET: CarrerasController
        public ActionResult Index()
        {
            return View(Carrera.Carreras);
        }

        public ActionResult Create()
        {

            var model = new CarreraViewModel();
            return View(model);
        }
        private void ValidateCarrera(CarreraViewModel carrera,bool nuevo)
        {
            if (nuevo)
             {
                if (carrera.Codigo == 0)
                {
                    ModelState.AddModelError(nameof(carrera.Codigo), "El codigo de la carrera no puede ser igual a  existe.");
                }
                if (Carrera.GetByCodigo(carrera.Codigo) != null )
                {
                    ModelState.AddModelError(nameof(carrera.Codigo), "El codigo de la carrera ya existe.");
                }

            }

            if (carrera.CantidadCreditos <= 0)
            {
                ModelState.AddModelError(nameof(carrera.CantidadCreditos), "La cantidad de creditos debe ser mayor que cero.");
            }
            if (carrera.CantidadMaterias <= 0)
            { 
                ModelState.AddModelError(nameof(carrera.CantidadMaterias), "La cantidad de materias debe ser mayor que cero.");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarreraViewModel carrera)
        {
            try
            {
                ValidateCarrera(carrera, true);

                if (ModelState.IsValid)  
                {

                    Carrera.Carreras.Add(carrera);  
                    ViewBag.Message = "Carrera creada exitosamente.";
                }
                else
                {
                        TempData["ErrorMessage"] = "Revisa los campos marcados en rojo.";
                        return View(carrera);
                }
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(carrera);
            }
        }

        [HttpGet("Carreras/Edit/{codigo}")]
        public ActionResult Edit(int codigo)
        {
            var est = Carrera.GetByCodigo(codigo);
            if (est == null)
            {
                TempData["ErrorMessage"] = "Carrera no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(est);
        }
        [HttpPost("Carreras/Edit/{codigo}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int codigo, CarreraViewModel model)
        {
            try
            {
                ValidateCarrera(model, false);

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Revisa los campos marcados en rojo.";
                    return View(model);
                }
                var actual = Carrera.GetByCodigo(codigo);
                if (actual == null)
                {
                    TempData["ErrorMessage"] = "Carrera no encontrada.";
                    return RedirectToAction(nameof(Index));
                }
                actual.Nombre = model.Nombre;
                actual.CantidadMaterias = model.CantidadMaterias;

                TempData["SuccessMessage"] = "Cambios guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        [HttpGet("Carreras/Details/{codigo}")]
        public ActionResult Details(int codigo)
        {
            var est = Carrera.GetByCodigo(codigo);
            if (est == null)
            {
                TempData["ErrorMessage"] = "Carrera no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(est);
        }


        [HttpGet("Carreras/Delete/{codigo}")]

        public ActionResult Delete(int codigo)
        {
            var est = Carrera.GetByCodigo(codigo);
            if (est == null)
            {
                TempData["ErrorMessage"] = "Carrera no encontrada.";
                return RedirectToAction(nameof(Index));
            }
            return View(est);
        }

        [HttpPost("Carreras/DeleteConfirmed/{codigo}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int codigo)
        {
            try
            {
                var rec = Carrera.GetByCodigo(codigo);
                if (rec != null)
                {
                    Carrera.Carreras.Remove(rec);
                    TempData["SuccessMessage"] = "Carrera eliminada correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se encontró la Carrera.";
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

