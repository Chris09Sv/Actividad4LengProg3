using System.ComponentModel.DataAnnotations;

namespace Actividad4LengProg3.Models
{
    public class CarreraViewModel
    {
        [Required]
        public int Codigo { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        public int CantidadCreditos { get; set; }
        [Required]
        public int CantidadMaterias   { get; set; }
    }
    public static class Carrera
    {
        public static List<CarreraViewModel> Carreras { get; } = new();

        public static CarreraViewModel? GetByCodigo(int id) =>
        Carreras.Where(e => e.Codigo == id).FirstOrDefault();


    }
}
