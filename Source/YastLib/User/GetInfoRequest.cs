using YastLib.Common;

namespace YastLib.User
{
    public class GetInfoRequest : YastRequest
    {
        public override string RequestType { get { return "user.getInfo"; } }

        public GetInfoRequest(YastAuthToken token)
            : base(token.User, token.Hash)
        {   
        }
    }
}