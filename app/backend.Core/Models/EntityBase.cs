using System.ComponentModel.DataAnnotations;
namespace backend.Core.Models
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime Added { get; set; } = DateTime.MinValue;
    }
}
