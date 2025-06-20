using FichaMedica.Modelos;

namespace FichaMedica.Repositorio.IRepositorio
{
	public interface IFichasRepositorio : IRepositorio<Ficha>
	{
		Task<Ficha> Actualizar(Ficha entidad);
	}
}
