using YastLib.Common;

namespace YastLib.User
{
    public class GetInfoRequest : YastRequest
    {
        public override string RequestType { get { return "user.getInfo"; } }

        public GetInfoRequest(string user, string hash)
            : base(user, hash)
        {   
        }
    }
}