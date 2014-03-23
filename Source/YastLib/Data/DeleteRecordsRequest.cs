using YastLib.Common;

namespace YastLib.Data
{
    public class DeleteRecordsRequest : YastRequest
    {
        public override string RequestType { get { return "data.delete"; } }

        public DeleteRecordsRequest(YastAuthToken token)
            : base(token.User, token.Hash)
        {
        }
    }
}