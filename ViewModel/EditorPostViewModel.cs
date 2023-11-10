using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoBlogApi.ViewModel;

public class EditorPostViewModel
{
    public EditorPostViewModel(string title, string summary, string body, string slug)
    {
        Title = title;
        Summary = summary;
        Body = body;
        Slug = slug;
        CreateDate = DateTime.Now.ToUniversalTime();
        LastUpdateDate = DateTime.Now.ToUniversalTime();
    }

    [DisplayName("Titulo")]
    [Required(ErrorMessage = "Por favor informar o título do post")]
    [StringLength(15), MinLength(5, ErrorMessage = "O título precisa ter no mínumo 5 e no máximo 20 caracteres")]   
    public string Title { get; set; } = null!;
  
    [DisplayName("Sumario")]
    [Required(ErrorMessage = "Por favor informar o sumário do post")]
    [MinLength(5, ErrorMessage = "O sumário precisa ter no mínimo 5 caracteres"),
    MaxLength(15, ErrorMessage = "O título precisa ter no máximo 15 caracteres")]
    public string Summary { get; set; } = null!;

    [DisplayName("Corpo do post")]
    [Required(ErrorMessage = "Por favor informar o corpo do post")]
    [MinLength(5, ErrorMessage = "O corpo do post precisa ter no mínimo 5 caracteres"),
    MaxLength(15, ErrorMessage = "O título precisa ter no máximo 100 caracteres")]
    public string Body { get; set; } = null!;

    [DisplayName("Slug")]
    [Required(ErrorMessage = "Por favor informar o Slug do post")]
    [MinLength(5, ErrorMessage = "O Slug precisa ter no mínimo 5 caracteres"),
    MaxLength(15, ErrorMessage = "O título precisa ter no máximo 15 caracteres")]
    public string Slug { get; set; } = null!;

    [DisplayName("Data de cricação")]
    [DataType(DataType.DateTime)]    
    public DateTime CreateDate { get; set; }

    [DisplayName("Data de Data de atualização")]
    [DataType(DataType.DateTime)]  
    public DateTime LastUpdateDate { get; set; }
}