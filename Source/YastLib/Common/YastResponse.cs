using System;
using System.Net.Http;
using System.Xml.Linq;

namespace YastLib.Common
{
    public abstract class YastResponse
    {
        public XElement Response { get; private set; }

        public Status Status
        {
            get { return (Status) GetResponseAttributeValue("status", 1); }
        }

        public int RequestId
        {
            get { return GetResponseAttributeValue("id", 0); }
        }

        protected YastResponse(HttpContent content)
        {
            var xdoc = XDocument.Parse(content.ReadAsStringAsync().Result);
            Response = xdoc.Element("response");
        }

        private T GetResponseAttributeValue<T>(string name, T defaultValue)
        {
            var xAttribute = Response.Attribute(name);
            if (xAttribute == null)
                return defaultValue;

            return (T)Convert.ChangeType(xAttribute.Value, typeof(T));
        }

        protected T GetResponseElementValue<T>(string name, T defaultValue)
        {
            var xElement = Response.Element(name);
            if (xElement == null)
                return defaultValue;

            return (T) Convert.ChangeType(xElement.Value, typeof (T));
        }
    }
}