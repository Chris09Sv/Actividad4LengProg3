using System.Diagnostics;
using Actividad4LengProg3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Actividad4LengProg3.Controllers
{
    public class EstudiantesController : Controller
    {
        // GET: EstudiantesController
        public ActionResult Index()
        {

            return View(EstudiantesL.Estudiantes);
        }

        // GET: EstudiantesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public void CargarCombos()
        {
            ViewBag.Generos = new SelectList(
    new[] { "Masculino", "Femenino" });

            ViewBag.Turno = new SelectList(
                new[] { "Mañana", "Tarde", "Noche" });

            ViewBag.Carrera = new SelectList(
                new[] {
                    "Ingeniera en Software","Ingenieria Ambiental"
                }
            );
            ViewBag.Recinto = new SelectList(
                    new[] {
                    "SDO","Metropolitano","Las Americas","Bani"
    }
);

            ViewBag.PorcentajesBeca = Enumerable.Range(0, 11)   // 0..10
                    .Select(i => i * 10)
                    .Select(v => new SelectListItem { Value = v.ToString(), Text = $"{v}%" })
                    .ToList();
        }
        // GET: EstudiantesController/Create
        public ActionResult Registrar()
        {

            //Recinto
            //    Carrera
            //Turno
            var model = new EstudiantesViewModel();
            CargarCombos();
            return View(model);

        }

        // POST: EstudiantesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(EstudiantesViewModel model)
        {
            try
            {
                if (!model.Becado) model.porcentaje_Beca =0 ;
                if(!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Revisa los campos marcados en rojo";
                    CargarCombos();
                    return View(model);
                }


                model.id = EstudiantesL.NextId();

                model.Matricula = EstudiantesL.NextMatricula(model);
                // Guardar en memoria
                EstudiantesL.Estudiantes.Add(model);

                TempData["SuccessMessage"] = "Se ha registrado el estudiante de manera exitosa.";
                // Patrón PRG: redirige para evitar reenvío del formulario
                return RedirectToAction(nameof(Registrar));
            }
            catch (Exception ex)
            {
                // Mensaje de error
                TempData["ErrorMessage"] = "No se pudo registrar el estudiante.";
                // (opcional) loguear ex
                CargarCombos();

                return View(model);
            }
        }

        // GET: EstudiantesController/Edit/5
        [HttpGet("Estudiantes/Edit/{matricula}")]

        public ActionResult Edit(string matricula)
        {
            var est = EstudiantesL.GetByMatricula(matricula);
            if (est == null)
            {
                TempData["ErrorMessage"] = "Estudiante no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            CargarCombos();
            return View(est);
        }

        // POST: EstudiantesController/Edit/5
        [HttpPost("Estudiantes/Edit/{matricula}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string matricula, EstudiantesViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Revisa los campos marcados en rojo.";
                    CargarCombos();
                    return View(model);
                }
                var actual = EstudiantesL.GetByMatricula(matricula);
                if (actual == null)
                {
                    TempData["ErrorMessage"] = "Estudiante no encontrado.";
                    return RedirectToAction(nameof(Index));
                }
                actual.Nombre_Completo = model.Nombre_Completo;
                actual.Carrera = model.Carrera;
                actual.Recinto = model.Recinto;
                actual.Correo_Institucional = model.Correo_Institucional;
                actual.Celular = model.Celular;
                actual.Telefono = model.Telefono;
                actual.Direccion = model.Direccion;
                actual.Fecha_Nacimiento = model.Fecha_Nacimiento;
                actual.Genero = model.Genero;
                actual.Turno = model.Turno;
                actual.Becado = model.Becado;
                actual.porcentaje_Beca = model.porcentaje_Beca;
                TempData["SuccessMessage"] = "Cambios guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: /Estudiantes/Delete/{matricula}
        [HttpGet("Estudiantes/Delete/{matricula}")]
        public ActionResult Delete(string matricula)
        {
            var est = EstudiantesL.GetByMatricula(matricula);
            if (est == null)
            {
                TempData["ErrorMessage"] = "Estudiante no encontrado.";
                return RedirectToAction(nameof(Index));
            }
            return View(est); // ← envía el modelo a la vista de confirmación
        }

        // POST: /Estudiantes/Delete/{matricula}
        [HttpPost("Estudiantes/Delete/{matricula}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string matricula)
        {
            try
            {
                var est = EstudiantesL.GetByMatricula(matricula);
                if (est != null)
                {
                    EstudiantesL.Estudiantes.Remove(est);
                    TempData["SuccessMessage"] = "Estudiante eliminado correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se encontró el estudiante.";
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
