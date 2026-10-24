using System.Net;
using System.Text.Json;
using ProjectoApi.Middleware;

namespace ProjectoApi.Middlewares
{
    public class MiddlewareExcepciones
    {
        private readonly RequestDelegate _siguiente; //Es un tipo de Delegado (tipo de dato) que permite procesar peticiones http

       // RequestDelegate significa que es un contrato. Le dice a C#: "Cualquier método que reciba un HttpContext 
       // y devuelva un Task puede ser considerado y tratado como un RequestDelegate."
        
        public MiddlewareExcepciones(RequestDelegate siguiente)//creamos el constructor de la Clase
        {
            _siguiente = siguiente;
        }

        public async Task InvokeAsync(HttpContext contexto) //Ejecuta tarea asincrona para procesar peticiones HTTP
        {
            try
            {
                // Deja que la petición pase hacia el AutoresController
                await _siguiente(contexto);
            }
            catch (Exception ex)
            {
                // ¡AQUÍ CAEN TODOS LOS ERRORES DE LA API!
                await ManejarErroresAsync(contexto, ex);
            }
        }

        private static Task ManejarErroresAsync(HttpContext contexto, Exception excepcion)
        {
            // Definimos que la respuesta siempre será JSON y código 400 (Bad Request)
            contexto.Response.ContentType = "application/json";
            contexto.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // Armamos el objeto con el formato estandarizado
            var respuesta = new RespuestaError
            {
                CodigoEstado = contexto.Response.StatusCode,
                Mensaje = excepcion.Message, // Texto de la excepción lanzada
                Ruta = contexto.Request.Path
            };

            // Convertimos a JSON y lo enviamos al cliente (Postman)
            var json = JsonSerializer.Serialize(respuesta); //convierte respuesta a formato Json
            return contexto.Response.WriteAsync(json); // se retorna Json al Postman
        }
    }
}