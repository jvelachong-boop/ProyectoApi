using System.Runtime.CompilerServices;
using BibliotecaApi.Entidades;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
    [ApiController]//Decoradod para realizar validaciones que envia usuario
    [Route("api/autores") ] //Ruta para llamar a las acciones de un controlador
    public class AutoresController:ControllerBase //Proporciona toda la funcionalidad essential para gestionar peticiones HTTP
    {
       private static List<Autor> _ListaAutores = new()
        {
             new Autor{Id=1,nombre="Jhorlly",edad =45},
             new Autor{Id=2,nombre="Roberto",edad =18},
             new Autor{Id=3,nombre="Alexander",edad =20},
             new Autor{Id=4,nombre="Andrea",edad =22},
             new Autor{Id=5,nombre="Rodriguez",edad =30},
             new Autor{Id=6,nombre="Roger",edad =15},
             new Autor{Id=7,nombre="Ana",edad =40}
        }; 
        
       [HttpGet("division/{numero1:int}/{numero2:int}")]
        public IActionResult Division(int numero1, int numero2)
        {
            try
            {
                decimal resultado = numero1 / numero2;
                return Ok(resultado);
            }
            catch (Exception ex){
               return BadRequest($"{ex.Message.ToString()}: Error de division entre 0");
            }            
        }

        [HttpGet]
        public List<Autor> Get()//IEnumerable permite recorrer o iterar sobre una colección de datos
        {
           
           return _ListaAutores;     
        }

        [HttpGet("{id:int}")]
        public ActionResult<Autor> Get(int id)//Metodo para devolver datos de Autor por ID
        {
            var datosAurtor = _ListaAutores.FirstOrDefault(x => x.Id==id);
            if (datosAurtor== null)
            {
                throw new Exception ($"El codigo de Autor: {id} ingresado no Existe, Verifique!!");
            }
            
            return Ok(datosAurtor);
        }

        [HttpGet("{nombre:alpha}")]//retorna lista de autores por coincidencia de nombres
        public ActionResult<Autor> Get(string nombre)
        {
         var datos = _ListaAutores.ToList().Where(x=> x.nombre.Contains(nombre));
         return Ok(datos);
        }
        

       [HttpGet("primero")]
        public ActionResult<Autor> ObtenerPrimero()//retorna lista de autores por ID
        {
            var autor = _ListaAutores.First();
            
            if (autor is null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpGet("ultimo")]
        public ActionResult<Autor> ObtenerUltimo()//retorna el ultimo Autor
        {
            var autor = _ListaAutores.Last();
            
            if (autor is null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpPost]
        public ActionResult Post(AutorDto dto)
        {
            var existeAutor = _ListaAutores.Any(x=> x.Id== dto.Id); // Se devuelve un valor booleano para validar existencia del Autor por ID
                    
            if (existeAutor){
                 throw new Exception ($"El codigo de Autor: {dto.Id} ingresado ya Existe, Verifique!!");
            }
            
            var nuevoAutor = new Autor  //Mapeo campos de la entidad con Entidad DTO
            {
                Id = dto.Id,
                nombre = dto.Nombre,
                edad = dto.edad,
                email = dto.Email,
                dni = dto.dni
            }; 
            
            _ListaAutores.Add(nuevoAutor);
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(Autor _autor)
        {
            var existeAutor = _ListaAutores.Any(x=> x.Id == _autor.Id);
            if (!existeAutor)
            {
                return BadRequest($"El Autor: {_autor.Id} no Existe, Verifique!!");   
            }

            var datosAutor = _ListaAutores.FirstOrDefault(x=> x.Id == _autor.Id);
            if (datosAutor is not null){
                datosAutor.nombre = _autor.nombre;
                datosAutor.edad = _autor.edad;
            }
            return Ok();
        }

        
        [HttpPatch]
        public ActionResult Patch(Autor _autor)
        {
            var existeAutor = _ListaAutores.Any(x=> x.Id == _autor.Id);
            if (!existeAutor)
            {
                return BadRequest($"El Autor: {_autor.Id} no Existe, Verifique!!");   
            }

            var datosAutor = _ListaAutores.FirstOrDefault(x=> x.Id == _autor.Id);
            
            if (datosAutor is not null){
                // Actualizamos ÚNICAMENTE las propiedades que el cliente envió
                if (datosAutor.nombre != null){
                    datosAutor.nombre = _autor.nombre;
                }
         
            }      
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int Id)
        {
            var existeAutor = _ListaAutores.Any(x=> x.Id == Id);
            if (!existeAutor)
            {
                return BadRequest($"El Autor a eliminar: {Id} no Existe, Verifique!!");   
            }

            var item = _ListaAutores.FirstOrDefault(x=> x.Id==Id);
            _ListaAutores.Remove(item);
            return NoContent();
        } 
    }
}
