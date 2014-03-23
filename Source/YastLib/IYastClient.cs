using System;
using System.Collections.Generic;
using YastLib.Common;

namespace YastLib
{
    public interface IYastClient
    {
        Auth.LoginResponse Login(string user, string password, int? requestId = null);

        User.GetUserInfoResponse GetUserInfo(YastAuthToken token, int? requestId = null);

        User.GetUserSettingsResponse GetUserSettings(YastAuthToken token);

        Data.GetRecordsResponse GetRecords(YastAuthToken token,
            DateTime? timeFrom = null, DateTime? timeTo = null, int? requestId = null);

        Data.GetFoldersResponse GetFolders(YastAuthToken token);

        Data.GetProjectsResponse GetProjects(YastAuthToken token);

        Report.GetReportResponse GetReport(YastAuthToken token, string reportFormat,
            DateTime? timeFrom = null, DateTime? timeTo = null, List<string> groupBy = null,
            int? requestId = null);

        Meta.GetRecordTypesResponse GetRecordTypes(YastAuthToken token);

        Data.AddRecordsResponse AddRecords(YastAuthToken token, params YastElement[] records);
    }
}