using AutoMapper;
using FichaMedica.Modelos;
using FichaMedica.Modelos.DTOs;
using FichaMedica.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Net;

namespace FichaMedica.Controllers
{
	[ApiController]
	public class FichasController : ControllerBase
	{
		private readonly IFichasRepositorio _fichasRepo;
		private readonly ILogger<FichasController> _logger;
		private readonly IMapper _mapper;
		protected APIResponse _response;
		public FichasController(ILogger<FichasController> logger, IFichasRepositorio fichasRepo, IMapper mapper)
		{
			_logger = logger;
			_mapper = mapper;
			_response = new();
			_fichasRepo = fichasRepo;
		}

		[HttpGet("Ficha/{dni}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> GetFichaByDni(string dni)
		{
			try
			{
				var ficha = await _fichasRepo.Obtener(x => x.Dni == dni);
				if (ficha == null)
				{
					_logger.LogError("GetFichaByDni Dni inexistente: " + dni);
					_response.IsExitoso = false;
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				_response.Resultado = _mapper.Map<Ficha>(ficha);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_logger.LogInformation("GetFichaByDni Error: " + dni + " / " + ex.ToString());
				_response.IsExitoso = false;
				_response.StatusCode = HttpStatusCode.InternalServerError;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpPost]
		[Route("Ficha")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CrearFicha([FromForm] FichaDTO CreateDTO)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogInformation("CrearFicha Error: Modelo invalido");
					return BadRequest(ModelState);
				}
				Ficha modelo = _mapper.Map<Ficha>(CreateDTO);

				var corroborarFicha = await _fichasRepo.Obtener(x => x.Dni == modelo.Dni);
				
				if (corroborarFicha != null)
				{
					_response.IsExitoso = false;
					_response.ErrorMessages = new List<string>() { "Ya existe una ficha con ese DNI." };
					_response.StatusCode = HttpStatusCode.Ambiguous;
					_response.Resultado = corroborarFicha;
				}

				if (modelo.Sexo == "M")
				{
					modelo.Sexo = "Masculino";
				}
				else if (modelo.Sexo == "F")
				{
					modelo.Sexo = "Femenino";
				}
				await _fichasRepo.Crear(modelo);
				_response.Resultado = modelo;
				_response.StatusCode = HttpStatusCode.Created;
			}
			catch (Exception ex)
			{
				_logger.LogInformation("CrearFicha Error: " + ex.ToString());
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}
	}
}
