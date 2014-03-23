using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.User
{
    public class GetUserSettingsResponse : YastCollectionResponse
    {
        public Dictionary<string, string> Settings;

        public GetUserSettingsResponse(XContainer xdoc)
            : base(xdoc)
        {
            Settings = new Dictionary<string, string>();

            var keys = GetElements("v", "keys").Select(k => k.Value).ToList();
            var values = GetElements("v", "values").Select(v => v.Value).ToList();

            Settings = Enumerable.Range(0, keys.Count).ToDictionary(i => keys[i], i => values[i]);
        }
    }
}