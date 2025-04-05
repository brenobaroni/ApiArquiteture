using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public abstract class Base
    {
        [Key]
        public int id { get; init; }
    }

    public abstract class  BaseData : Base
    {
        public DateTime create_at { get; init; }
        public DateTime? update_at { get; set; }
    }
}
