using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entidades
{
    public class Autor
    {
        public int Id {get;set;}
        [Required]
        public  string nombre {get; set;}= string.Empty;
        public  int edad {get; set;}
        public string email { get; set; } = string.Empty;
        public string dni {get;set;} = string.Empty;
    }
}