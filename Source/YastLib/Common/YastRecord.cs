using System;
using System.Xml.Linq;
using YastLib.Data;

namespace YastLib.Common
{
    public class YastRecord : YastElement
    {
        public int TypeId
        {
            get { return Element.GetElementValue("typeId", 0); }
            set { Element.SetElementValue("typeId", value); }
        }

        public DateTime TimeCreated
        {
            get { return YastTime.FromSecondsSince1970(Element.GetElementValue("timeCreated", 0D)); }
            set { Element.SetElementValue("timeCreated", YastTime.ToSecondsSince1970(value)); }
        }

        public DateTime TimeUpdated
        {
            get { return YastTime.FromSecondsSince1970(Element.GetElementValue("timeUpdated", 0D)); }
            set { Element.SetElementValue("timeUpdated", YastTime.ToSecondsSince1970(value)); }
        }

        public int ProjectId
        {
            get { return Element.GetElementValue("project", 0); }
            set { Element.SetElementValue("project", value); }
        }

        public int CreatorId
        {
            get { return Element.GetElementValue("creator", 0); }
            set { Element.SetElementValue("creator", value); }
        }

        protected YastRecord() :
            base("record")
        {
            Element.Add(new XElement("typeId", 0));
            Element.Add(new XElement("timeCreated", 0));
            Element.Add(new XElement("timeUpdated", 0));
            Element.Add(new XElement("project", 0));
            Element.Add(new XElement("variables"));
            Element.Add(new XElement("creator", 0));
        }

        protected YastRecord(XElement record)
            : base(record)
        {
        }

        private XElement Variables
        {
            get { return Element.Element("variables"); }
        }

        protected string GetVariableValue(int index)
        {
            int currentIndex = 0;
            foreach (var xVar in Variables.Elements("v"))
            {
                if (currentIndex == index)
                    return xVar.Value;
                currentIndex++;
            }

            return null;
        }

        protected void SetVariableValue(int index, string value)
        {
            int currentIndex = 0;
            foreach (var xVar in Variables.Elements("v"))
            {
                if (currentIndex == index)
                {
                    xVar.SetValue(value);
                    break;
                }

                currentIndex++;
            }
        }

        public static explicit operator YastRecord(XElement record)
        {
            int typeId = record.GetElementValue("typeId", 0);

            switch (typeId)
            {
                case 1: return new YastWorkRecord(record);
                case 3: return new YastPhonecallRecord(record);
                default: return new YastRecord(record);
            }
        }
    }
}
