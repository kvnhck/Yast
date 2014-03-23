using System;
using System.Xml.Linq;

namespace YastLib.Common
{
    public class YastProject : YastElement
    {
        public string Name
        {
            get { return Element.GetElementValue("name", string.Empty); }
            set { Element.SetElementValue("name", value); }
        }

        public string Description
        {
            get { return Element.GetElementValue("description", string.Empty); }
            set { Element.SetElementValue("description", value); }
        }

        public string PrimaryColor
        {
            get { return Element.GetElementValue("primaryColor", string.Empty); }
            set { Element.SetElementValue("primaryColor", value); }
        }

        public int ParentId
        {
            get { return Element.GetElementValue("parentId", 0); }
            set { Element.SetElementValue("parentId", value); }
        }

        public YastPrivilege Privileges
        {
            get { return (YastPrivilege)Element.GetElementValue("privileges", 0); }
            set { Element.SetElementValue("privileges", (int)value); }
        }

        public DateTime TimeCreated
        {
            get { return YastTime.FromSecondsSince1970(Element.GetElementValue("timeCreated", 0D)); }
            set { Element.SetElementValue("timeCreated", YastTime.ToSecondsSince1970(value)); }
        }

        public int CreatorId
        {
            get { return Element.GetElementValue("creator", 0); }
            set { Element.SetElementValue("creator", value); }
        }

        public YastProject(XElement project)
            : base(project)
        {
        }

        public YastProject()
            : base("project")
        {

        }

        public static explicit operator YastProject(XElement project)
        {
            return new YastProject(project);
        }
    }
}