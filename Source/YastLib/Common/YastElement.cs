using System.Xml.Linq;

namespace YastLib.Common
{
    public abstract class YastElement
    {
        private readonly XElement _element;
        
        public XElement Element { get { return _element; } }

        public int Id
        {
            get { return _element.GetElementValue("id", 0); }
            set { _element.SetElementValue("id", value); }
        }

        protected YastElement(XName name)
        {
            _element = new XElement(name,
                new XAttribute("id", 0));
        }

        protected YastElement(XElement element)
        {
            _element = element;
        }
    }
}