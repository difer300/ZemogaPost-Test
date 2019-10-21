using System.ComponentModel.DataAnnotations;

namespace Zemoga.Models
{
    public enum PostStatus
    {
        [Display(Name = "New")]
        New = 0,

        [Display(Name = "Pending publish approval")]
        PendingPublishApproval = 1,

        [Display(Name = "Published")]
        Published = 2
    }

}
