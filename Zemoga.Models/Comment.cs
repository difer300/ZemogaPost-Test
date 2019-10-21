namespace Zemoga.Models
{
    public class Comment : Entity
    {
        public string Text { get; set; }

        public string AuthorName { get; set; }

        public string AuthorEmail { get; set; }

        public Post Post { get; set; }
    }
}
