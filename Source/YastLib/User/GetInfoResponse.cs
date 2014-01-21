using System;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.User
{
    public class GetInfoResponse : YastResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime TimeCreated { get; private set; }
        public bool ValidSubscription { get; private set; }

        public GetInfoResponse(HttpContent content)
            : base(content)
        {
        }

        protected override void ProcessResult(XDocument result)
        {
            base.ProcessResult(result);

            int id;
            if (int.TryParse(result.Root.Elements("id").FirstOrDefault().Value, out id))
                Id = id;

            Name = result.Root.Elements("name").FirstOrDefault().Value;

            double timeCreatedSeconds;
            if (double.TryParse(result.Root.Elements("timeCreated").FirstOrDefault().Value, out timeCreatedSeconds))
                TimeCreated = YastTime.FromSecondsSince1970(timeCreatedSeconds);

            int validSubscription;
            if (int.TryParse(result.Root.Elements("subscription").FirstOrDefault().Value, out validSubscription))
                ValidSubscription = validSubscription == 1;
        }
    }
}