using Newtonsoft.Json;

namespace AzureCosmosDbClientApi.Models
{
    public class UserViewModel : ContainerViewModel
    {
        public UserViewModel()
        {
            Id = Guid.NewGuid();    
        }
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("UserName")]
        public string UserName { get; set; } = string.Empty;
        [JsonProperty("LastName")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("UserEmail")]
        public string UserEmail { get; set; } = string.Empty;
    }
}
