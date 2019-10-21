using System;

namespace Zemoga.Models.Interfaces
{
    public interface ITimestamped
    {
        DateTime ModifiedAt { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
