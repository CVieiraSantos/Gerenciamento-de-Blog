using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoBlogApi.ViewModel;

[ApiController]
public class EditorRoleViewModel : ControllerBase
{
    public EditorRoleViewModel(string name, string slug)
    {
        Name = name;
        Slug = slug;
    }

    [Required(ErrorMessage = "Por favor insira o nome do perfil")]
    [DisplayName("Nome do perfil")]    
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Por favor insira o Slug")]    
    public string Slug { get; set; } = null!;
}