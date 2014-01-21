using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Auth
{
    public class LoginRequest : YastRequest
    {
        public override string RequestType { get { return "auth.login"; } }

        public string Password { get; set; }

        public LoginRequest(string user, string password)
            : base(user, null)
        {
            Password = password;
        }

        public override XDocument ToXml()
        {
            var xml = base.ToXml();

            xml.Root.Add(new XElement("password", Password));

            return xml;
        }
    }
}