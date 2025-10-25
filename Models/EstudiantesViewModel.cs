using Actividad4LengProg3.Models;
using System.ComponentModel.DataAnnotations;

namespace Actividad4LengProg3.Models
{
    public class EstudiantesViewModel
    {
        public int id { get; set; }
        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} no puede superar {1} caracteres.")]
        
        public string Nombre_Completo { get; set; }

        //[Display(Name = "Matrícula")]
        //[Required(ErrorMessage = "La {0} es obligatoria.")]
        //[StringLength(15, MinimumLength = 6,
        //    ErrorMessage = "La {0} debe tener entre {2} y {1} caracteres.")]
        public string Matricula { get; set; } = "";

        [Display(Name = "Carrera")]

        [Required(ErrorMessage = "La {0} es obligatoria.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Selecciona una {0} válida.")]
        public string Carrera { get; set; }

        [Display(Name = "Recinto")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        //[Range(1, int./*MaxValue*/, ErrorMessage = "Selecciona un {0} válido.")]
        public string Recinto { get; set; }

        [Display(Name = "Correo institucional")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El {0} no tiene un formato válido.")]
        public string Correo_Institucional { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [Phone(ErrorMessage = "El {0} no es válido.")]
        [StringLength(15, MinimumLength = 10,
            ErrorMessage = "El {0} debe tener entre {2} y {1} dígitos.")]
        public string Celular { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [Phone(ErrorMessage = "El {0} no es válido.")]
        [StringLength(15, MinimumLength = 10,
            ErrorMessage = "El {0} debe tener entre {2} y {1} dígitos.")]
        public string Telefono { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(200, ErrorMessage = "La {0} no puede superar {1} caracteres.")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }   

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Nacimiento { get; set; }

        [Display(Name = "Género")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string Genero { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string Turno { get; set; } // Mañana / Tarde / Noche

        [Display(Name = "Becado")]
        [Required(ErrorMessage = "Indica si el estudiante está {0}.")]
        public bool Becado { get; set; }

        [Display(Name = "Porcentaje de beca")]
        [Range(0, 100, ErrorMessage = "El {0} debe estar entre {1} y {2}.")]
        public int porcentaje_Beca { get; set; }
    }
}



public static class EstudiantesL
{
    public static  List<EstudiantesViewModel> Estudiantes  { get; } = new();

    private static int _seq = 1;

    public static int NextId() => _seq++;

    private static int _matSeq = 0;
    private static int _matYear = DateTime.Now.Year;

    private static readonly Dictionary<string, string> _codigoRecinto = new(StringComparer.OrdinalIgnoreCase)
    {
        ["SDO"] = "SD",
        ["Metropolitano"] = "MT",
        ["Las Americas"] = "LA",
        ["Bani"] = "BN"
    };
    private static string CodeOrDefault(Dictionary<string, string> map, string? key, string def)
    => key != null && map.TryGetValue(key, out var val) ? val : def;


    public static string NextMatricula (EstudiantesViewModel model)
    {
        var nowYear = DateTime.Now.Year;
        if (nowYear != _matYear)
        {
            _matYear = nowYear;
            _matSeq = 0;
        }

        var recinto = CodeOrDefault(_codigoRecinto, model.Recinto, "RC");
        var sec = Interlocked.Increment(ref _matSeq);

        return $"{recinto}-{nowYear}-{sec:0000}";
    }


    public static EstudiantesViewModel? GetById(int id) =>
    Estudiantes.FirstOrDefault(e => e.id == id);

    public static EstudiantesViewModel? GetByMatricula (string? matricula)
    {
        if(string.IsNullOrWhiteSpace(matricula))
            return null;
        var key = matricula.Trim();
        return Estudiantes.FirstOrDefault(e=> !string.IsNullOrWhiteSpace(e.Matricula) && string.Equals(e.Matricula.Trim(),key,StringComparison.OrdinalIgnoreCase) );
    }
}