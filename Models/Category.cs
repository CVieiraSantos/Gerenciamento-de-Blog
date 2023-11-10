
namespace Blog.Models
{

    public class Category
    {     
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public IList<Post> Posts { get; set; } = null!;
    }
}
