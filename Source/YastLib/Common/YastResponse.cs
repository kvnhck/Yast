using System.Diagnostics;
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

        protected YastResponse(XContainer xdoc)
        {
            Response = xdoc.Element("response");

            Trace.TraceInformation(Response.ToString(SaveOptions.DisableFormatting));
        }
    }
}