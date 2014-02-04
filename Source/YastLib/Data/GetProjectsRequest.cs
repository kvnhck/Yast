using YastLib.Common;

namespace YastLib.Data
{
    public class GetProjectsRequest : YastRequest
    {
        public override string RequestType { get { return "data.getProjects"; } }

        public GetProjectsRequest(YastAuthToken token)
            : base(token.User, token.Hash)
        {
        }
    }
}