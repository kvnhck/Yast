using System;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.User
{
    public class GetUserInfoResponse : YastResponse
    {
        public int Id
        {
            get { return Response.GetElementValue("id", 0); }
        }

        public string Name
        {
            get { return Response.GetElementValue("name", string.Empty); }
        }

        public DateTime TimeCreated
        {
            get { return YastTime.FromSecondsSince1970(Response.GetElementValue("timeCreated", 0D)); }
        }

        public bool ValidSubscription
        {
            get { return Response.GetElementValue("subscription", 0) == 1; }
        }

        public GetUserInfoResponse(XContainer xdoc)
            : base(xdoc)
        {
        }
    }
}