using System;
using System.Collections.Generic;
using System.Xml.Linq;
using YastLib.Data;

namespace YastLib.Common
{
    public class YastRecord
    {
        private readonly XElement _record;

        public int Id { get; set; }
        public int TypeId { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public int ProjectId { get; set; }
        public IList<string> Variables { get; set; }
        public int CreatorId { get; set; }

        public YastRecord(int typeId)
        {
            TypeId = typeId;
            Variables = new List<string>();
        }

        private static YastRecord GetByTypeId(int typeId)
        {
            switch (typeId)
            {
                case 1: return new WorkRecord();
                case 3: return new PhonecallRecord();
                default: return new YastRecord(typeId);
            }
        }

        public static YastRecord ConvertFrom(XElement record)
        {
            int typeId = record.GetElementValue("typeId", 0);

            YastRecord result = GetByTypeId(typeId);
            result.ProcessRecord(record);

            return result;
        }

        protected virtual void ProcessRecord(XElement record)
        {
            Id = record.GetElementValue("id", 0);

            TimeCreated = YastTime.FromSecondsSince1970(
                record.GetElementValue("timeCreated", 0D));

            TimeUpdated = YastTime.FromSecondsSince1970(
                record.GetElementValue("timeUpdated", 0D));

            ProjectId = record.GetElementValue("project", 0);

            CreatorId = record.GetElementValue("creator", 0);

            var xVariables = record.Element("variables");
            if (xVariables != null)
            {
                foreach(var variable in xVariables.Elements("v"))
                    Variables.Add(variable.Value);
            }
        }
    }
}
