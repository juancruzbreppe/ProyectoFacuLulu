﻿using System.Linq.Expressions;

namespace FichaMedica.Repositorio.IRepositorio
{
	public interface IRepositorio<T> where T : class
	{
		Task Crear(T entidad);
		Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null);
		Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true);
		Task Remover(T entidad);
		Task Grabar();
	}
}
