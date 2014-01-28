using System;
using System.Xml.Linq;

namespace YastLib.Common
{
    public class YastFolder
    {
        private readonly XElement _folder;

        public int Id
        {
            get { return _folder.GetElementValue("id", 0); }
        }

        public string Name
        {
            get { return _folder.GetElementValue("name", string.Empty); }
        }

        public string Description
        {
            get { return _folder.GetElementValue("description", string.Empty); }
        }

        public string PrimaryColor
        {
            get { return _folder.GetElementValue("primaryColor", string.Empty); }
        }

        public int ParentId
        {
            get { return _folder.GetElementValue("parentId", 0); }
        }

        public YastPrivilege Privileges
        {
            get { return (YastPrivilege) _folder.GetElementValue("privileges", 0); }
        }

        public DateTime TimeCreated
        {
            get { return YastTime.FromSecondsSince1970(_folder.GetElementValue("timeCreated", 0D)); }
        }

        public int CreatorId
        {
            get { return _folder.GetElementValue("creator", 0); }
        }

        public YastFolder(XElement folder)
        {
            _folder = folder;
        }
    }
}