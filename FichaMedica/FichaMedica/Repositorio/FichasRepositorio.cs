using FichaMedica.Datos;
using FichaMedica.Modelos;
using FichaMedica.Repositorio.IRepositorio;

namespace FichaMedica.Repositorio
{
	public class FichasRepositorio : Repositorio<Ficha>, IFichasRepositorio
	{
		private readonly ApplicationDBContext _db;
		public FichasRepositorio(ApplicationDBContext db) : base(db)
		{
			_db = db;
		}
		public async Task<Ficha> Actualizar(Ficha entidad)
		{
			_db.Fichas.Update(entidad);
			await _db.SaveChangesAsync();
			return entidad;
		}
	}
}
