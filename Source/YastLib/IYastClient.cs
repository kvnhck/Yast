using System;
using System.Collections.Generic;
using YastLib.Auth;
using YastLib.Common;
using YastLib.Data;
using YastLib.Meta;
using YastLib.Report;
using YastLib.User;

namespace YastLib
{
    public interface IYastClient
    {
        LoginResponse Login(string user, string password, int? requestId = null);

        GetInfoResponse GetUserInfo(YastAuthToken token, int? requestId = null);

        GetRecordsResponse GetRecords(YastAuthToken token,
            DateTime? timeFrom = null, DateTime? timeTo = null, int? requestId = null);

        GetFoldersResponse GetFolders(YastAuthToken token);

        GetProjectsResponse GetProjects(YastAuthToken token);

        GetReportResponse GetReport(YastAuthToken token, string reportFormat,
            DateTime? timeFrom = null, DateTime? timeTo = null, List<string> groupBy = null,
            int? requestId = null);

        GetRecordTypesResponse GetRecordTypes(YastAuthToken token);

        AddRecordsResponse AddRecords(YastAuthToken token, params YastElement[] records);
    }
}