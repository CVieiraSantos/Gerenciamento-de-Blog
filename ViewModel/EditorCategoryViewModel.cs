using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoBlogApi.ViewModel
{
    public class EditorCategoryViewModel
    {
        public EditorCategoryViewModel(string name, string slug)
        {
            Name = name;
            Slug = slug;
            CreatedDate = DateTime.Now.ToUniversalTime();
        }

        [DisplayName("Nome da categoria")]
        [Required(ErrorMessage = "Por favor digite o nome da categoria")]
        [MinLength(5, ErrorMessage = "Por favor insira uma categoria que tenha no mínumo 5 caracteres")
        , MaxLength(10, ErrorMessage = "O nome da categoria precisa ter no mínimo 10 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo Slug é obrigatório")]
        public string Slug { get; set; }
        public DateTime CreatedDate {get; private set; }


    }
}