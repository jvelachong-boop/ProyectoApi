// 1. Inicializa el constructor de la aplicación web, configurando los ajustes por defecto
using ProjectoApi.Middlewares;

var builder = WebApplication.CreateBuilder();//

// 2. Agrega al contenedor los servicios para utilizar controladores en la Web API.
builder.Services.AddControllers();

// 3. Construye la instancia de la aplicación (App)
var App = builder.Build();

// 4. Mapea la ruta de los controladores para que ASP.NET  sepa cómo responder a peticiones HTTP 
// mediante los atributos [Route] y [HttpGet/HttpPost] de los controladores.

App.UseMiddleware<MiddlewareExcepciones>();
App.MapControllers();

//5.Inicia el servidor y escicha las peticiones
App.Run();
