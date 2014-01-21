using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.User
{
    public class GetInfoRequest : YastRequest
    {
        public override string RequestType { get { return "user.getInfo"; } }

        public string User { get; set; }

        public GetInfoRequest(string user, string hash)
            : base(user, hash)
        {
            User = user;
        }

        public override XDocument ToXml()
        {
            var xml = base.ToXml();

            xml.Root.Add(new XElement("user", User));

            return xml;
        }
    }
}