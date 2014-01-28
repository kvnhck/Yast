using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using YastLib.Auth;
using YastLib.Common;
using YastLib.Data;
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

        private HttpResponseMessage Post(YastRequest request)
        {
            using (var client = CreateClient())
            {
                var response = client.PostAsync("", request.ToHttpContent()).Result;
                response.EnsureSuccessStatusCode();
                return response;
            }
        }

        public LoginResponse Login(string user, string password, int? requestId = null)
        {
            var request = new LoginRequest(user, password) { RequestId = requestId };
            
            var response = Post(request);

            return new LoginResponse(response.Content);
        }

        public GetInfoResponse GetUserInfo(string user, string hash, int? requestId = null)
        {
            var request = new GetInfoRequest(user, hash) { RequestId = requestId };
            
            var response = Post(request);

            return new GetInfoResponse(response.Content);
        }

        public GetRecordsResponse GetRecords(string user, string hash,
            DateTime? timeFrom = null, DateTime? timeTo = null, int? requestId = null)
        {
            var request = new GetRecordsRequest(user, hash) { RequestId = requestId };

            if (timeFrom.HasValue) request.TimeFrom = timeFrom;
            if (timeTo.HasValue) request.TimeTo = timeTo;

            var response = Post(request);

            return new GetRecordsResponse(response.Content);
        }

        public GetReportResponse GetReport(string user, string hash, string reportFormat,
            DateTime? timeFrom = null, DateTime? timeTo = null, List<string> groupBy = null,
            int? requestId = null)
        {
            var request = new GetReportRequest(user, hash, reportFormat) { RequestId = requestId };

            if (timeFrom.HasValue) request.TimeFrom = timeFrom;
            if (timeTo.HasValue) request.TimeTo = timeTo;
            if (groupBy != null) request.GroupBy = groupBy;

            var response = Post(request);

            return new GetReportResponse(response.Content);
        }
    }
}
