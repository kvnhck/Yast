using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class GetProjectsResponse : YastCollectionResponse
    {
        private IEnumerable<YastProject> _projects;
        public IEnumerable<YastProject> Projects
        {
            get { return _projects ?? (_projects = GetProjects()); }
        }

        public GetProjectsResponse(XContainer xdoc)
            : base(xdoc)
        {
        }

        private IEnumerable<YastProject> GetProjects()
        {
            return GetObjects("project").Select(o => (YastProject) o);
        }
    }
}