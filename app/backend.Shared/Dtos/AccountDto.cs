using System.ComponentModel.DataAnnotations;
using backend.Shared.Validations;
using Newtonsoft.Json;

namespace backend.Shared.Dtos
{
    public class AccountDto
    {
        [RequiredGUIDAttribute(ErrorMessage = "account_id missing or has incorrect type.")]
        [Newtonsoft.Json.JsonProperty("account_id", Required = Newtonsoft.Json.Required.Default)]
        public string? Id { get; set; } = default!;

        [Required]
        [Newtonsoft.Json.JsonProperty("balance", Required = Newtonsoft.Json.Required.Always)]
        public int Balance { get; set; }
    }
}
