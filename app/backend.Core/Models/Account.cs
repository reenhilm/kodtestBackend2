using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace backend.Core.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        [Required]
        public int Balance { get; set; }

        [Required]
        public DateTimeOffset Added { get; set; }

        //Nav prop
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
