using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Newtonsoft.Json;

namespace backend.Shared.Dtos
{
    public class TransactionDto
    {
        [Required]
        [Newtonsoft.Json.JsonProperty("transaction_id", Required = Newtonsoft.Json.Required.Always)]
        public Guid Id { get; set; }
        
        [Required]
        [Newtonsoft.Json.JsonProperty("amount", Required = Newtonsoft.Json.Required.Always)]
        public int Amount { get; set; }                

        [Required]
        [Newtonsoft.Json.JsonProperty("created_at", Required = Newtonsoft.Json.Required.Always)]
        public System.DateTime Added { get; set; }

        public AccountDto? Account { get; set; }
    }
}