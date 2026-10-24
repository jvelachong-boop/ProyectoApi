using System.ComponentModel.DataAnnotations;
public class AutorDto
{
    public int Id {get;set;}
    
    [Required(ErrorMessage ="El Nombre es Obligatorio")]
    [StringLength(50, MinimumLength =2, ErrorMessage ="El Nombre debe tener entre 2 y 50 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    
    [Range(18,99,ErrorMessage ="Debes tener entre 18 y 99 años para registrarte")]
    public  int edad {get; set;}
    
    [Required(ErrorMessage ="El email es obligatorio")]
    [EmailAddress(ErrorMessage ="debe de ingresar un correo Valido")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage ="El DNI es obligatorio")]
    [StringLength(8,MinimumLength =8,ErrorMessage ="Numero de DNI Invalido")]
    [RegularExpression(@"^\d{8}$",ErrorMessage = "El Dni debe ser un valor numerico")]  
    
    /// <summary> Validacion de DNI
    //    ^   : Inicio de la cadena de texto    
    //    \d  : Cualquier dígito numérico (0 al 9)    
    //    {8} : Repetir exactamente 8 veces  
    //    $   : Fin de la cadena de texto  
    /// </summary>
    public string dni {get;set;} = string.Empty;

}