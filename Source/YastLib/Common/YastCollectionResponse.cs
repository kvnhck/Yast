using System.Collections.Generic;
using System.Xml.Linq;

namespace YastLib.Common
{
    public abstract class YastCollectionResponse : YastResponse
    {
        protected YastCollectionResponse(XContainer xdoc)
            : base(xdoc)
        {
        }

        protected IEnumerable<XElement> GetObjects(string elementName)
        {
            var xObjects = Response.Element("objects");
            if (xObjects == null) yield break;

            foreach (var element in xObjects.Elements(elementName))
                yield return element;
        }
    }
}