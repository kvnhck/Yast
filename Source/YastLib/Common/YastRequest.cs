using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Xml.Linq;

namespace YastLib.Common
{
    public abstract class YastRequest
    {
        public abstract string RequestType { get; }

        public int? RequestId { get; set; }

        public string User { get; set; }

        public string Hash { get; set; }

        public YastRequest(string user, string hash)
        {
            User = user;
            Hash = hash;
        }

        public virtual XDocument ToXml()
        {
            var xml = new XDocument(
                new XElement("request",
                    new XAttribute("req", RequestType),
                    new XAttribute("id", RequestId.GetValueOrDefault(0)),
                    new XElement("user", User),
                    new XElement("hash", Hash)));

            return xml;
        }

        public override string ToString()
        {
            return ToXml().ToString();
        }

        public HttpContent ToHttpContent()
        {
            var values = new Dictionary<string, string>();

            var request = ToXml().ToString(SaveOptions.DisableFormatting);

            Trace.TraceInformation(request);
            
            values.Add("request", request);

            return new FormUrlEncodedContent(values);
        }
    }
}