using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Report
{
    public class GetReportResponse : YastResponse
    {
        public int ReportId { get; private set; }

        public string ReportHash { get; private set; }

        public string FileType { get; private set; }

        public GetReportResponse(HttpContent content)
            : base(content)
        {
        }

        protected override void ProcessResult(XDocument result)
        {
            base.ProcessResult(result);

            int reportId;
            if (int.TryParse(result.Root.Elements("reportId").FirstOrDefault().Value, out reportId))
                ReportId = reportId;

            ReportHash = result.Root.Elements("reportHash").FirstOrDefault().Value;

            FileType = result.Root.Elements("fileType").FirstOrDefault().Value;
        }

        public Uri GetDownloadUrl(Uri baseUri, string user, string hash)
        {
            return new Uri(
                baseUri,
                string.Format("/file.php?type=report&id={0}&hash={1}&user={2}&userhash={3}",
                    ReportId,
                    ReportHash,
                    HttpUtility.UrlEncode(user),
                    hash));
        }
    }
}