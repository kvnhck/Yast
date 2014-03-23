using System;
using System.Globalization;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class PhonecallRecord : YastRecord
    {
        public DateTime StartTime
        {
            get { return YastTime.FromSecondsSince1970(int.Parse(GetVariableValue(0))); }
            set { SetVariableValue(0, YastTime.ToSecondsSince1970(value).ToString(CultureInfo.InvariantCulture)); }
        }
        
        public DateTime? EndTime
        {
            get
            {
                return (!IsRunning)
                    ? YastTime.FromSecondsSince1970(int.Parse(GetVariableValue(1)))
                    : (DateTime?)null;
            }
            set
            {
                SetVariableValue(1,
                    value.HasValue
                        ? YastTime.ToSecondsSince1970(value.Value).ToString(CultureInfo.InvariantCulture)
                        : "0");
            }
        }

        public string Comment
        {
            get { return GetVariableValue(2); }
            set { SetVariableValue(2, value); }
        }

        public bool IsRunning
        {
            get { return GetVariableValue(3) == "1"; }
            set {  SetVariableValue(3, value ? "1" : "0"); }
        }

        public string PhoneNumber
        {
            get { return GetVariableValue(4); }
            set { SetVariableValue(4, value); }
        }

        public bool Outgoing
        {
            get { return GetVariableValue(5) == "1"; }
            set { SetVariableValue(5, value ? "1" : "0"); }
        }

        public PhonecallRecord(XElement record)
            : base(record)
        {
        }

        public PhonecallRecord()
        {
            TypeId = 3;

            var xVariables = Element.Element("variables");
            if (xVariables == null)
            {
                xVariables = new XElement("variables");
                Element.Add(xVariables);
            }

            xVariables.Add(new XElement("v", 0)); //StartTime
            xVariables.Add(new XElement("v", 0)); //EndTime
            xVariables.Add(new XElement("v", "")); //Comment
            xVariables.Add(new XElement("v", 0)); //IsRunning
            xVariables.Add(new XElement("v", "")); //PhoneNumber
            xVariables.Add(new XElement("v", 0)); //Outgoing
        }
    }
}