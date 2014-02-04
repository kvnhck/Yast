using System.Xml.Linq;

namespace YastLib.Common
{
    public abstract class YastElement
    {
        protected XElement _element;

        public int Id
        {
            get { return _element.GetElementValue("id", 0); }
        }

        protected YastElement(XElement element)
        {
            _element = element;
        }
    }
}