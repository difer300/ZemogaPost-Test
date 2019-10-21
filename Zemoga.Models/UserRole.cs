using System.ComponentModel.DataAnnotations;

namespace Zemoga.Models
{
    public enum UserRole
    {
        [Display(Name = "Writer")]
        Writer = 0,

        [Display(Name = "Editor")]
        Editor = 1
    }

}
