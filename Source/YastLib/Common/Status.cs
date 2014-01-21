namespace YastLib.Common
{
    public enum Status
    {
        Success = 0,
        Unknown = 1,
        AccessDenied = 3,
        NotLoggedIn = 4,
        LoginFailure = 5,
        InvalidInput = 6,
        SubscriptionRequired = 7,
        DataFormatError = 8,
        NoRequest = 9,
        InvalidRequest = 10,
        MissingFields = 11,
        RequestTooLarge = 12,
        ServerMaintenance = 13,
        InvalidRequestLanguage = 14,
  
        DuplicateItem = 100,
        InsufficientPrivileges = 101,
        UnknownRecordtype = 200,
        UnknownProject = 201,
        UnknownFolder = 202,
        UnknownRecord = 203,
        ParentIsSelf = 204,
        VariabletypeMismatch = 205,
        UnknownSetting = 300,
        InvalidSettingValue = 301,
        PasswordFormatInvalid = 800,
        UnknownReportFormat = 801,
        UnknownGroupbyValue = 802,
    }
}