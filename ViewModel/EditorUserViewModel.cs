using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace ProjetoBlogApi.ViewModel;

public class EditorUserViewModel
{
    public EditorUserViewModel(int id, string name, string email, string passwordhash)
    {
        Id = id;
        Name = name;
        Email = email;    
        PasswordHash = passwordhash;    
    }
        

    public int Id { get; set; }    
    [Required(ErrorMessage = "Insirar um nome de usuário")]    
    [DisplayName("Nome do usuário")]
    [MinLength(3, ErrorMessage = "O nome de usuário precisa ter no mínimo 3 caracteres"), MaxLength(8,
    ErrorMessage = "O nome de usuário precisa ter no máximo 15 caracteres")]
    public string Name { get; set; } = null!;       

    [Required(ErrorMessage = "Insirar um email válido")]     
    [DisplayName("Email do usuário")]
    [EmailAddress(ErrorMessage = "Formato de E-mail inválido")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Por favor inserir a senha de usuário")]
    [StringLength(15, MinimumLength = 6,
     ErrorMessage ="A senha do usuário precisa ter no mínimo 6 e no máximo 15 caracteres")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[A-Z])(?=.*[!#@$%&])(?=.*[0-9])(?=.*[a-z]).{6,15}$", 
    ErrorMessage = "A senha deve ter somente letras, no mínumo um numeros e caractere especial(!#@$%&)")]
    public string PasswordHash { get; set; } = null!;    

}