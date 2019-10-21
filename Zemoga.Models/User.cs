namespace Zemoga.Models
{
    public class User : Entity
    {
        public string Name { get; set; }

        public UserRole Role { get; set; }
    }
}
