using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class GetFoldersResponse : YastCollectionResponse
    {
        private IEnumerable<YastFolder> _folders;
        public IEnumerable<YastFolder> Folders
        {
            get { return _folders ?? (_folders = GetProjects()); }
        }

        public GetFoldersResponse(XContainer xdoc)
            : base(xdoc)
        {
        }

        private IEnumerable<YastFolder> GetProjects()
        {
            return GetObjects("folder").Select(o => (YastFolder) o);
        }
    }
}