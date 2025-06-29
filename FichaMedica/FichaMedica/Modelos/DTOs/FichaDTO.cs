namespace FichaMedica.Modelos.DTOs
{
	public class FichaDTO
	{
		public string Dni { get; set; }

		// Datos personales
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public string Sexo { get; set; }

		public int? AlturaCm { get; set; }
		public decimal? PesoKg { get; set; }
		public string? Email { get; set; }
		public string? Telefono { get; set; }

		// Sección 2
		public string? CondicionCronica { get; set; }
		public string? CondicionesCronicasDetalle { get; set; }
		public string? Alergia { get; set; }
		public string? AlergiasDetalle { get; set; }
		public string? MedicacionRegular { get; set; }
		public string? MedicacionRegularDetalle { get; set; }
		public string? CirugiaReciente { get; set; }
		public string? CirugiaRecienteDetalle { get; set; }
		public string? Fuma { get; set; }
		public string? FumaDetalle { get; set; }
		public string? Droga { get; set; }
		public string? DrogaDetalle { get; set; }

		// Sección 4
		public string? GrupoSanguineo { get; set; }
		public string? InformacionAdicional { get; set; }
		public string? NombreContactoEmergencia { get; set; }
		public string? TelefonoContactoEmergencia { get; set; }
		public bool AceptaTerminos { get; set; }
	}
}
