using System.ComponentModel.DataAnnotations;

namespace backend.Core.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public DateTime Added { get; set; } = DateTime.MinValue;

        //FK
        public Guid AccountId { get; set; }
    }
}
