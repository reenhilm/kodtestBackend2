using System.ComponentModel.DataAnnotations;

namespace backend.Core.Models
{
    public class Transaction : EntityBase
    {
        [Required]
        public int Amount { get; set; }
        public Account? Account { get; set; }
    }
}
