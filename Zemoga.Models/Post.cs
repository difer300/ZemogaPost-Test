namespace Zemoga.Models
{
    public class Post : Entity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public PostStatus Status { get; set; }
    }
}
