using System.Net.Http;
using System.Xml.Linq;

namespace YastLib.Common
{
    public abstract class YastResponse
    {
        public Status Status { get; protected set; }

        public int RequestId { get; protected set; }

        public YastResponse(HttpContent content)
        {
            var result = XDocument.Parse(content.ReadAsStringAsync().Result);

            ProcessResult(result);
        }

        protected virtual void ProcessResult(XDocument result)
        {
            Status status;
            if (Status.TryParse(result.Root.Attribute("status").Value, out status))
                Status = status;

            int id;
            if (int.TryParse(result.Root.Attribute("id").Value, out id))
                RequestId = id;
        }
    }
}