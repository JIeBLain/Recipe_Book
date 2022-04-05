namespace Client.ClientResult
{
    public class ClientResult
    {
        public ClientResult(int statusCode, ClientError error = null)
        {
            StatusCode = statusCode;
            Error = error;
        }

        public int StatusCode { get; }

        public ClientError Error { get; }

        public void EnsureSuccess()
        {
            if (!IsSuccessful())
            {
                throw new ClientException(StatusCode, Error);
            }
        }

        public bool IsSuccessful()
        {
            return !(Error != null || StatusCode is < 200 or > 300);
        }
    }

    public sealed class ClientResult<TResponse> : ClientResult
    {
        private readonly TResponse response;

        public ClientResult(int statusCode, TResponse response) : base(statusCode)
        {
            this.response = response;
        }

        public ClientResult(int statusCode, ClientError error = null) : base(statusCode, error)
        {
        }

        public TResponse Response
        {
            get
            {
                EnsureSuccess();
                return response;
            }
        }
    }
}