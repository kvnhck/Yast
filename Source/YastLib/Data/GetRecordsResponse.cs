using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using YastLib.Common;

namespace YastLib.Data
{
    public class GetRecordsResponse : YastResponse
    {
        private IEnumerable<YastRecord> _records;
        public IEnumerable<YastRecord> Records
        {
            get { return _records ?? (_records = GetRecords()); }
        }

        public GetRecordsResponse(HttpContent content)
            : base(content)
        {
        }

        private IEnumerable<YastRecord> GetRecords()
        {
            var xObjects = Response.Element("objects");
            if (xObjects == null) yield break;
            
            foreach (var record in xObjects.Elements("record").Select(YastRecord.ConvertFrom))
                yield return record;
        } 
    }
}