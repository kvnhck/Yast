using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class AddRecordsResponse : YastCollectionResponse
    {
        public IEnumerable<YastRecord> Records { get; private set; }

        public IEnumerable<YastProject> Projects { get; private set; }

        public IEnumerable<YastFolder> Folders { get; private set; }

        public AddRecordsResponse(XContainer xdoc)
            : base(xdoc)
        {
            Records = GetElements("record").Select(x => (YastRecord) x);
            Projects = GetElements("project").Select(x => (YastProject) x);
            Folders = GetElements("folder").Select(x => (YastFolder) x);
        }
    }
}