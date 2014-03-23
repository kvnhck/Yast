using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class DeleteRecordsResponse : YastResponse
    {
        public DeleteRecordsResponse(XContainer xdoc)
            : base(xdoc)
        {
        }
    }
}