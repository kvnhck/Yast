using System;
using System.Net.Http;
using YastLib.Common;

namespace YastLib.User
{
    public class GetInfoResponse : YastResponse
    {
        public int Id
        {
            get { return GetResponseElementValue("id", 0); }
        }

        public string Name
        {
            get { return GetResponseElementValue("name", string.Empty); }
        }

        public DateTime TimeCreated
        {
            get { return YastTime.FromSecondsSince1970(GetResponseElementValue("timeCreated", 0D)); }
        }

        public bool ValidSubscription
        {
            get { return GetResponseElementValue("subscription", 0) == 1; }
        }

        public GetInfoResponse(HttpContent content)
            : base(content)
        {
        }
    }
}