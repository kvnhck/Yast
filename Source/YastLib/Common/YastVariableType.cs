using System.Xml.Linq;

namespace YastLib.Common
{
    public class YastVariableType : YastElement
    {
        public string Name
        {
            get { return Element.GetElementValue("name", string.Empty); }
        }

        public YastVariableType(XElement variableType)
            : base(variableType)
        {
        }

        public static explicit operator YastVariableType(XElement variableType)
        {
            return new YastVariableType(variableType);
        }
    }
}