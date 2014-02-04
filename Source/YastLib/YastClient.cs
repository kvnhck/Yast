using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using YastLib.Auth;
using YastLib.Common;
using YastLib.Data;
using YastLib.Meta;
using YastLib.Report;
using YastLib.User;

namespace YastLib
{
    public class YastClient
    {
        private readonly Uri _baseAddress;

        public YastClient(string baseAddress)
        {
            _baseAddress = new Uri(new Uri(baseAddress), "/1.0/");
        }

        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = _baseAddress;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            return httpClient;
        }

        private XDocument ParseResponse(HttpResponseMessage response)
        {
            return XDocument.Parse(response.Content.ReadAsStringAsync().Result);
        }

        private XDocument Post(YastRequest request)
        {
            using (var client = CreateClient())
            {
                var response = client.PostAsync("", request.ToHttpContent()).Result;
                response.EnsureSuccessStatusCode();
                return ParseResponse(response);
            }
        }

        public LoginResponse Login(string user, string password, int? requestId = null)
        {
            var request = new LoginRequest(user, password) { RequestId = requestId };
            
            var response = Post(request);

            return new LoginResponse(response);
        }

        public GetInfoResponse GetUserInfo(YastAuthToken token, int? requestId = null)
        {
            var request = new GetInfoRequest(token) { RequestId = requestId };
            
            var response = Post(request);

            return new GetInfoResponse(response);
        }

        public GetRecordsResponse GetRecords(YastAuthToken token,
            DateTime? timeFrom = null, DateTime? timeTo = null, int? requestId = null)
        {
            var request = new GetRecordsRequest(token) { RequestId = requestId };

            if (timeFrom.HasValue) request.TimeFrom = timeFrom;
            if (timeTo.HasValue) request.TimeTo = timeTo;

            var response = Post(request);

            return new GetRecordsResponse(response);
        }

        public GetFoldersResponse GetFolders(YastAuthToken token)
        {
            var request = new GetFoldersRequest(token);

            var response = Post(request);

            return new GetFoldersResponse(response);
        }

        public GetProjectsResponse GetProjects(YastAuthToken token)
        {
            var request = new GetProjectsRequest(token);

            var response = Post(request);

            return new GetProjectsResponse(response);
        }

        public GetReportResponse GetReport(YastAuthToken token, string reportFormat,
            DateTime? timeFrom = null, DateTime? timeTo = null, List<string> groupBy = null,
            int? requestId = null)
        {
            var request = new GetReportRequest(token, reportFormat) { RequestId = requestId };

            if (timeFrom.HasValue) request.TimeFrom = timeFrom;
            if (timeTo.HasValue) request.TimeTo = timeTo;
            if (groupBy != null) request.GroupBy = groupBy;

            var response = Post(request);

            return new GetReportResponse(response);
        }

        public GetRecordTypesResponse GetRecordTypes(YastAuthToken token)
        {
            var request = new GetRecordTypesRequest(token);

            var response = Post(request);

            return new GetRecordTypesResponse(response);
        }
    }
}
