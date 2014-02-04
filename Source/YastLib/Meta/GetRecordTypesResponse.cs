using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Meta
{
    public class GetRecordTypesResponse : YastCollectionResponse
    {
        private IEnumerable<YastRecordType> _recordTypes;
        public IEnumerable<YastRecordType> RecordTypes
        {
            get { return _recordTypes ?? (_recordTypes = GetRecordTypes()); }
        }

        public GetRecordTypesResponse(XContainer xdoc)
            : base(xdoc)
        {
        }

        private IEnumerable<YastRecordType> GetRecordTypes()
        {
            return GetObjects("recordType").Select(o => (YastRecordType) o);
        }
    }
}