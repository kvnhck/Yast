using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class AddRecordsRequest : YastRequest
    {
        public override string RequestType { get { return "data.add"; } }

        public IList<YastElement> Objects { get; private set; }

        public AddRecordsRequest(YastAuthToken token, params YastElement[] objects)
            : base(token.User, token.Hash)
        {
            Objects = new List<YastElement>(objects);
        }

        public override XDocument ToXml()
        {
            var xml = base.ToXml();

            xml.Root.Add(new XElement("objects", Objects.Select(o => CleanElement(o.Element))));

            return xml;
        }

        private XElement CleanElement(XElement element)
        {
            var copy = new XElement(element);

            copy.Attribute("id").Remove();

            var timeCreated = copy.Element("timeCreated");
            if (timeCreated != null) timeCreated.Remove();

            var timeUpdated = copy.Element("timeUpdated");
            if (timeUpdated != null) timeUpdated.Remove();

            var creator = copy.Element("creator");
            if (creator != null) creator.Remove();

            return copy;
        }
    }
}