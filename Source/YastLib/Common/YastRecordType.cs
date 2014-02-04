using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YastLib.Common
{
    public class YastRecordType : YastElement
    {
        public string Name
        {
            get { return _element.GetElementValue("name", string.Empty); }
        }

        private IList<YastVariableType> _variableTypes;

        public IList<YastVariableType> VariableTypes
        {
            get { return _variableTypes ?? (_variableTypes = GetVariableTypes()); }
        }

        public YastRecordType(XElement recordType)
            : base(recordType)
        {
        }

        private IList<YastVariableType> GetVariableTypes()
        {
            var xVariableTypes = _element.Element("variableTypes");
            if (xVariableTypes == null) return new YastVariableType[0];

            return xVariableTypes.Elements("variableType")
                .Select(v => (YastVariableType) v)
                .ToList();
        }

        public static explicit operator YastRecordType(XElement recordType)
        {
            return new YastRecordType(recordType);
        }
    }
}