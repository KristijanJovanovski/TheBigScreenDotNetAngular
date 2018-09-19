using System;

namespace TheBigScreen.Entities.Entities
{
    public abstract class BaseEntity
    {
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
