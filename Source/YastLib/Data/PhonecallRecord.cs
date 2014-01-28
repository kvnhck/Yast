using System;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class PhonecallRecord : YastRecord
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Comment { get; set; }
        public bool IsRunning { get; set; }
        public string PhoneNumber { get; set; }
        public bool Outgoing { get; set; }

        public PhonecallRecord() : base(3)
        {
        }

        protected override void ProcessRecord(XElement record)
        {
            base.ProcessRecord(record);

            StartTime = YastTime.FromSecondsSince1970(int.Parse(Variables[0]));
            Comment = Variables[2];
            IsRunning = Variables[3] == "1";
            PhoneNumber = Variables[4];
            Outgoing = Variables[5] == "1";

            if (!IsRunning)
                EndTime = YastTime.FromSecondsSince1970(int.Parse(Variables[1]));
        }
    }
}