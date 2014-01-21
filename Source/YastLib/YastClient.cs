using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using YastLib.Auth;
using YastLib.Common;
using YastLib.Report;
using YastLib.User;

namespace YastLib
{
    /// <summary>
    /// <see cref="https://github.com/yastdotcom/yastlibs/blob/master/php/yastlib.php"/>
    /// </summary>
    public class YastClient
    {
        private Uri baseAddress;

        public YastClient(string baseAddress)
        {
            this.baseAddress = new Uri(new Uri(baseAddress), "/1.0/");
        }

        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
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

        public LoginResponse Login(string user, string password)
        {
            var request = new LoginRequest(user, password);

            var response = Post(request);

            return new LoginResponse(response.Content);
        }

        public GetInfoResponse GetUserInfo(string user, string hash)
        {
            var request = new GetInfoRequest(user, hash);

            var response = Post(request);

            return new GetInfoResponse(response.Content);
        }

        public GetReportResponse GetReport(string user, string hash, string reportFormat,
            DateTime? timeFrom = null, DateTime? timeTo = null, List<string> groupBy = null)
        {
            var request = new GetReportRequest(user, hash, reportFormat);

            if (timeFrom.HasValue) request.TimeFrom = timeFrom;
            if (timeTo.HasValue) request.TimeTo = timeTo;
            if (groupBy != null) request.GroupBy = groupBy;

            var response = Post(request);

            return new GetReportResponse(response.Content);
        }
    }
}
