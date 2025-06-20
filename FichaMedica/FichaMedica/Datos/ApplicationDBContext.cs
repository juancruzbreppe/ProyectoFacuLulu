using FichaMedica.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FichaMedica.Datos
{
	public class ApplicationDBContext : DbContext
	{
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
		{

		}
		public DbSet<Ficha> Fichas { get; set; }
	}
}
