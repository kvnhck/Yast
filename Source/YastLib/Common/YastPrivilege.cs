using System;

namespace YastLib.Common
{
    [Flags]
    public enum YastPrivilege
    {
        None = 0,
        Owner = 1,
        Read = 2,
        Modify = 4,
        Delete = 8,
        //Reserved = 16,
        WorkerRole = 32,
        ProjectManagerRole = 64,
        AdministratorRole = 128
    }
}