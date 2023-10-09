using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace backend.Shared.Dtos
{
    public class AccountDto
    {
        [Required]
        [Newtonsoft.Json.JsonProperty("account_id", Required = Newtonsoft.Json.Required.Always)]
        public Guid Id { get; set; }

        [Required]
        [Newtonsoft.Json.JsonProperty("balance", Required = Newtonsoft.Json.Required.Always)]
        public int Balance { get; set; }
    }
}
