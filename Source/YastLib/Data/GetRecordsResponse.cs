using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class GetRecordsResponse : YastCollectionResponse
    {
        private IEnumerable<YastRecord> _records;
        public IEnumerable<YastRecord> Records
        {
            get { return _records ?? (_records = GetRecords()); }
        }

        public GetRecordsResponse(XContainer xdoc)
            : base(xdoc)
        {
        }

        private IEnumerable<YastRecord> GetRecords()
        {
            return GetObjects("record").Select(o => (YastRecord) o);
        } 
    }
}