using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FichaMedica.Modelos
{
	public class Ficha
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Dni { get; set; }

		// Datos personales
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public string Sexo { get; set; }

		// Datos médicos
		public string? GrupoSanguineo { get; set; }
		public bool EsDiabetico { get; set; }
		public bool EsAlergico { get; set; }
		public string? Alergias { get; set; } // Puede ser un texto libre
		public string? Enfermedades { get; set; }
		public string? MedicacionActual { get; set; }

		// Contacto en caso de emergencia
		public string? NombreContactoEmergencia { get; set; }
		public string? TelefonoContactoEmergencia { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.UtcNow.AddHours(-3);
	}
}
