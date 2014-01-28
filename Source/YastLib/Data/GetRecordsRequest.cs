using System;
using System.Collections.Generic;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class GetRecordsRequest : YastRequest
    {
        public override string RequestType { get { return "data.getRecords"; } }

        public IList<int> TypeId { get; set; }
        public IList<int> ProjectId { get; set; }
        public IList<int> ParentId { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }

        public GetRecordsRequest(string user, string hash)
            : base(user, hash)
        {
            TypeId = new List<int>();
            ProjectId = new List<int>();
            ParentId = new List<int>();
        }

        public override XDocument ToXml()
        {
            var xml = base.ToXml();

            if (TypeId.Count > 0)
                xml.Root.Add(new XElement("typeId", string.Join(",", TypeId)));

            if (ProjectId.Count > 0)
                xml.Root.Add(new XElement("projectId", string.Join(",", ProjectId)));

            if (ParentId.Count > 0)
                xml.Root.Add(new XElement("parentId", string.Join(",", ParentId)));

            if (TimeFrom.HasValue)
                xml.Root.Add(new XElement("timeFrom", YastTime.ToSecondsSince1970(TimeFrom.Value)));

            if (TimeTo.HasValue)
                xml.Root.Add(new XElement("timeTo", YastTime.ToSecondsSince1970(TimeTo.Value)));

            return xml;
        }
    }
}