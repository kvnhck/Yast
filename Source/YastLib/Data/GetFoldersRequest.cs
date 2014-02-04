using YastLib.Common;

namespace YastLib.Data
{
    public class GetFoldersRequest : YastRequest
    {
        public override string RequestType { get { return "data.getFolders"; } }

        public GetFoldersRequest(YastAuthToken token)
            : base(token.User, token.Hash)
        {
        }
    }
}