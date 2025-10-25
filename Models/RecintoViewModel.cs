using System.ComponentModel.DataAnnotations;

namespace Actividad4LengProg3.Models
{
    public class RecintoViewModel
    {
        [Required]
        public int Codigo { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }
    }
    public static class Recinto
    {
        public static List<RecintoViewModel> Recintos { get; } = new();

        public static RecintoViewModel? GetByCodigo(int id) =>
        Recintos.Where(e => e.Codigo == id).FirstOrDefault();


    }
}
