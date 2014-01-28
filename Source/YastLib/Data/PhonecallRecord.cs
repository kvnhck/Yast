﻿using System;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Data
{
    public class PhonecallRecord : YastRecord
    {
        public DateTime StartTime
        {
            get { return YastTime.FromSecondsSince1970(int.Parse(Variables[0])); }
        }
        
        public DateTime? EndTime
        {
            get
            {
                return (!IsRunning)
                    ? YastTime.FromSecondsSince1970(int.Parse(Variables[1]))
                    : (DateTime?)null;
            }
        }

        public string Comment
        {
            get { return Variables[2]; }
        }

        public bool IsRunning
        {
            get { return Variables[3] == "1"; }
        }

        public string PhoneNumber
        {
            get { return Variables[4]; }
        }

        public bool Outgoing
        {
            get { return Variables[5] == "1"; }
        }

        public PhonecallRecord(XElement record)
            : base(record)
        {
        }
    }
}