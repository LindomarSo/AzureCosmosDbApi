using Newtonsoft.Json;

namespace AzureCosmosDbClientApi.Models
{
    public class UserModel
    {
        public UserModel(Guid id, string userName, string lastName, string userEmail)
        {
            Id = id;
            UserName = userName;
            LastName = lastName;
            UserEmail = userEmail;
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
