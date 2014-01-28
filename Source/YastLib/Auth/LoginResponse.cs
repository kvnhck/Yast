using System.Net.Http;
using YastLib.Common;

namespace YastLib.Auth
{
    public class LoginResponse : YastResponse
    {
        public string Hash
        {
            get { return Response.GetElementValue("hash", string.Empty); }
        }

        public LoginResponse(HttpContent content)
            : base(content)
        {
        }
    }
}