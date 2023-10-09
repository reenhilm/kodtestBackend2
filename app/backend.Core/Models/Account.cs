using System.ComponentModel.DataAnnotations;

namespace backend.Core.Models
{
    public class Account : EntityBase
    {
        [Required]
        public int Balance { get; set; }
    }
}
