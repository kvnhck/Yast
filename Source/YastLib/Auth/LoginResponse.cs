using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Auth
{
    public class LoginResponse : YastResponse
    {
        public string Hash { get; private set; }

        public LoginResponse(HttpContent content)
            : base(content)
        {
            
        }

        protected override void ProcessResult(XDocument result)
        {
            base.ProcessResult(result);

            Hash = result.Root.Elements("hash").FirstOrDefault().Value;
        }
    }
}