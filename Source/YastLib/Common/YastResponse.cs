using System.Net.Http;
using System.Xml.Linq;

namespace YastLib.Common
{
    public abstract class YastResponse
    {
        public XElement Response { get; private set; }

        public Status Status
        {
            get { return (Status) Response.GetAttributeValue("status", 1); }
        }

        public int RequestId
        {
            get { return Response.GetAttributeValue("id", 0); }
        }

        protected YastResponse(HttpContent content)
        {
            var xdoc = XDocument.Parse(content.ReadAsStringAsync().Result);
            Response = xdoc.Element("response");
        }
    }
}