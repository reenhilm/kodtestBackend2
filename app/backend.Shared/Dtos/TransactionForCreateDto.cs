using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Newtonsoft.Json;
using backend.Shared.Validations;

namespace backend.Shared.Dtos
{
    public class TransactionForCreateDto
    {
        [Required]
        [Newtonsoft.Json.JsonProperty("amount", Required = Newtonsoft.Json.Required.Always)]
        public int Amount { get; set; }

        [RequiredGUIDAttribute(ErrorMessage = "account_id missing or has incorrect type.")]
        [Newtonsoft.Json.JsonProperty("account_id", Required = Newtonsoft.Json.Required.Default)]
        public string? AccountId { get; set; } = default!;
    }
}