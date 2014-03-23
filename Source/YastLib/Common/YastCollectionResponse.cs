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

        protected IEnumerable<XElement> GetElements(string elementName, string collectionElement = "objects")
        {
            var collection = Response.Element(collectionElement);
            if (collection == null) yield break;

            foreach (var element in collection.Elements(elementName))
                yield return element;
        }
    }
}