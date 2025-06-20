using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text;
using FichaMedica.Datos;
using FichaMedica.Repositorio.IRepositorio;
using FichaMedica.Repositorio;
using FichaMedica;
using FichaMedica.Modelos;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*PARA PRODUCCION
 * var connectionString = builder.Configuration.GetConnectionString("ServidorPROD");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
	options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 37)))); */

var connectionString = builder.Configuration.GetConnectionString("ServidorPROD");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
	options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 37))));


builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IFichasRepositorio, FichasRepositorio>();
builder.Services.AddCors(options => {
	options.AddPolicy("PoliticaCors",
		builder =>
		{
			builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
		});
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		.AddCookie(options =>
		{
			options.Cookie.HttpOnly = true;
			options.ExpireTimeSpan = TimeSpan.FromDays(7);
			options.SlidingExpiration = true;
		});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//soporte para cors
app.UseCors("PoliticaCors");

app.UseRouting();

//soporte para autenticacion
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.Run();