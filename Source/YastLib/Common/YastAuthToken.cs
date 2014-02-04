namespace YastLib.Common
{
    public class YastAuthToken
    {
        public string User { get; private set; }

        public string Hash { get; private set; }

        public YastAuthToken(string user, string hash)
        {
            User = user;
            Hash = hash;
        }
    }
}