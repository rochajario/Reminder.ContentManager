using System.ComponentModel.DataAnnotations;

namespace ContentManager.Data.Entities
{
    public abstract class BaseModel : ICloneable
    {
        [Key]
        public Guid Id { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
