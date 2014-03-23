using YastLib.Common;

namespace YastLib.Data
{
    public class ChangeRecordsRequest : YastRequest
    {
        public override string RequestType { get { return "data.change"; } }

        public ChangeRecordsRequest(YastAuthToken token)
            : base(token.User, token.Hash)
        {
        }
    }
}