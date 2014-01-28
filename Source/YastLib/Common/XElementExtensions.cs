using System;
using System.Xml.Linq;

namespace YastLib.Common
{
    public static class XElementExtensions
    {
        public static T GetElementValue<T>(this XElement element, string name, T defaultValue)
        {
            var xElement = element.Element(name);
            if (xElement == null) return defaultValue;

            return (T)Convert.ChangeType(xElement.Value, typeof(T));
        }

        public static T GetAttributeValue<T>(this XElement element, string name, T defaultValue)
        {
            var xAttribute = element.Attribute(name);
            if (xAttribute == null) return defaultValue;

            return (T)Convert.ChangeType(xAttribute.Value, typeof(T));
        }
    }
}