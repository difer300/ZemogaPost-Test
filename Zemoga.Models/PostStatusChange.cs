namespace Zemoga.Models
{
    public class PostStatusChange : Entity
    {
        public Post Post { get; set; }
        public PostStatus Status { get; set; }
        public User User { get; set; }

    }
}
