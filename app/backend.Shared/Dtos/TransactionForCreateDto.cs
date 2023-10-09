using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Newtonsoft.Json;

namespace backend.Shared.Dtos
{
    public class TransactionForCreateDto
    {
        [Required]
        [Newtonsoft.Json.JsonProperty("amount", Required = Newtonsoft.Json.Required.Always)]
        public int Amount { get; set; }

        [Required]
        [Newtonsoft.Json.JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public Guid AccountId { get; set; }
    }
}