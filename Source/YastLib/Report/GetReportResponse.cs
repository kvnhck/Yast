using System;
using System.Web;
using System.Xml.Linq;
using YastLib.Common;

namespace YastLib.Report
{
    public class GetReportResponse : YastResponse
    {
        public int ReportId
        {
            get { return Response.GetElementValue("reportId", 0); }
        }

        public string ReportHash
        {
            get { return Response.GetElementValue("reportHash", string.Empty); }
        }

        public string FileType
        {
            get { return Response.GetElementValue("fileType", string.Empty); }
        }

        public GetReportResponse(XContainer xdoc)
            : base(xdoc)
        {
        }

        public Uri GetDownloadUrl(Uri baseUri, YastAuthToken token)
        {
            return new Uri(
                baseUri,
                string.Format("/file.php?type=report&id={0}&hash={1}&user={2}&userhash={3}",
                    ReportId,
                    ReportHash,
                    HttpUtility.UrlEncode(token.User),
                    token.Hash));
        }
    }
}