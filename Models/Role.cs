namespace Blog.Models
{
    public class Role
    {     
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;

        public IList<User> Users { get; set; } = null!;
    }
}