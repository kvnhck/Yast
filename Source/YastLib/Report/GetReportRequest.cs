using System;
using System.Collections.Generic;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Report
{
    public class GetReportRequest : YastRequest
    {
        public override string RequestType { get { return "report.getReport"; } }

        public string ReportFormat { get; set; }
        public IList<int> TypeId { get; set; }
        public IList<int> ParentId { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public IList<string> GroupBy { get; set; }
        public IList<string> Constraints { get; set; }

        public GetReportRequest(string user, string hash, string reportFormat)
            : base(user, hash)
        {
            ReportFormat = reportFormat;

            TypeId = new List<int>();
            ParentId = new List<int>();
            GroupBy = new List<string>();
            Constraints = new List<string>();
        }

        public override XDocument ToXml()
        {
            var xml = base.ToXml();

            xml.Root.Add(new XElement("reportFormat", ReportFormat));
            
            if (TypeId.Count > 0)
                xml.Root.Add(new XElement("typeId", string.Join(",", TypeId)));

            if (ParentId.Count > 0)
                xml.Root.Add(new XElement("parentId", string.Join(",", ParentId)));

            if (TimeFrom.HasValue)
                xml.Root.Add(new XElement("timeFrom", YastTime.ToSecondsSince1970(TimeFrom.Value)));

            if (TimeTo.HasValue)
                xml.Root.Add(new XElement("timeTo", YastTime.ToSecondsSince1970(TimeTo.Value)));

            if (GroupBy.Count > 0)
                xml.Root.Add(new XElement("groupBy", string.Join(",", GroupBy)));

            if (Constraints.Count > 0)
                xml.Root.Add(new XElement("constraints", string.Join(",", Constraints)));

            return xml;
        }
    }
}