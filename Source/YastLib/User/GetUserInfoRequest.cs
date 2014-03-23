using YastLib.Common;

namespace YastLib.User
{
    public class GetUserInfoRequest : YastRequest
    {
        public override string RequestType { get { return "user.getInfo"; } }

        public GetUserInfoRequest(YastAuthToken token)
            : base(token.User, token.Hash)
        {   
        }
    }
}