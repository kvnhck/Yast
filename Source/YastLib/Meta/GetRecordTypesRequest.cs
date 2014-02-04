using YastLib.Common;

namespace YastLib.Meta
{
    public class GetRecordTypesRequest : YastRequest
    {
        public override string RequestType { get { return "meta.getRecordTypes"; } }

        public GetRecordTypesRequest(YastAuthToken token)
            : base(token.User, token.Hash)
        {
        }
    }
}