using System.Text.Json.Serialization;

namespace Client.ClientResult
{
    public sealed class ClientError
    {
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public sealed class ErrorIds
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}