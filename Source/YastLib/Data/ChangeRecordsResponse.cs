using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class ChangeRecordsResponse : YastResponse
    {
        public ChangeRecordsResponse(XContainer xdoc)
            : base(xdoc)
        {
        }
    }
}