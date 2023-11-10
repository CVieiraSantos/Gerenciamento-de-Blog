namespace Blog.Models
{
    public class Post
    {     
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Category Category { get; set; } = null!;        
        public User Author { get; set; } = null!;        
        public IList<Tag> Tags { get; set; } = null!;
    }
}