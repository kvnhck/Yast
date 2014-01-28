using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using YastLib.Data;

namespace YastLib.Common
{
    public class YastRecord
    {
        private readonly XElement _record;

        public int Id
        {
            get { return _record.GetElementValue("id", 0); }
        }

        public int TypeId
        {
            get { return _record.GetElementValue("typeId", 0); }
        }

        public DateTime TimeCreated
        {
            get { return YastTime.FromSecondsSince1970(_record.GetElementValue("timeCreated", 0D)); }
        }

        public DateTime TimeUpdated
        {
            get { return YastTime.FromSecondsSince1970(_record.GetElementValue("timeUpdated", 0D)); }
        }

        public int ProjectId
        {
            get { return _record.GetElementValue("project", 0); }
        }

        private IList<string> _variables;

        public IList<string> Variables
        {
            get { return _variables ?? (_variables = GetVariables()); }
        }

        public int CreatorId
        {
            get { return _record.GetElementValue("creator", 0); }
        }

        protected YastRecord(XElement record)
        {
            _record = record;
        }

        public static YastRecord ConvertFrom(XElement record)
        {
            int typeId = record.GetElementValue("typeId", 0);

            switch (typeId)
            {
                case 1: return new WorkRecord(record);
                case 3: return new PhonecallRecord(record);
                default: return new YastRecord(record);
            }
        }

        private IList<string> GetVariables()
        {
            var xVariables = _record.Element("variables");
            if (xVariables == null) return new string[0];

            return xVariables.Elements("v")
                .Select(variable => variable.Value)
                .ToList();
        }
    }
}
