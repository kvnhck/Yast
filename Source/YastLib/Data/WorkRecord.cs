using System;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class WorkRecord : YastRecord
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Comment { get; set; }
        public bool IsRunning { get; set; }

        public WorkRecord() : base(1)
        {
        }

        protected override void ProcessRecord(XElement record)
        {
            base.ProcessRecord(record);

            StartTime = YastTime.FromSecondsSince1970(int.Parse(Variables[0]));
            Comment = Variables[2];
            IsRunning = Variables[3] == "1";

            if (!IsRunning)
                EndTime = YastTime.FromSecondsSince1970(int.Parse(Variables[1]));
        }
    }
}