﻿using System;
using System.Xml.Linq;

namespace YastLib.Common
{
    public class YastFolder : YastElement
    {
        public string Name
        {
            get { return _element.GetElementValue("name", string.Empty); }
        }

        public string Description
        {
            get { return _element.GetElementValue("description", string.Empty); }
        }

        public string PrimaryColor
        {
            get { return _element.GetElementValue("primaryColor", string.Empty); }
        }

        public int ParentId
        {
            get { return _element.GetElementValue("parentId", 0); }
        }

        public YastPrivilege Privileges
        {
            get { return (YastPrivilege)_element.GetElementValue("privileges", 0); }
        }

        public DateTime TimeCreated
        {
            get { return YastTime.FromSecondsSince1970(_element.GetElementValue("timeCreated", 0D)); }
        }

        public int CreatorId
        {
            get { return _element.GetElementValue("creator", 0); }
        }

        public YastFolder(XElement folder)
            : base(folder)
        {
        }

        public static explicit operator YastFolder(XElement folder)
        {
            return new YastFolder(folder);
        }
    }
}