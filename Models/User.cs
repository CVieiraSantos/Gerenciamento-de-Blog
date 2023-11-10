using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class User
    {            
        public int Id { get; set; }        
        public string Name { get; set; } = null!;        
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Bio { get; set; } = null!;

        public IList<Post> Posts { get; set; } = null!;
        public IList<Role> Roles { get; set; } = null!;

    }
}