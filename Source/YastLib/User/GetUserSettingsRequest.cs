using YastLib.Common;

namespace YastLib.User
{
    public class GetUserSettingsRequest : YastRequest
    {
        public override string RequestType { get { return "user.getSettings"; } }

        public GetUserSettingsRequest(YastAuthToken token)
            : base(token.User, token.Hash)
        {
        }
    }
}