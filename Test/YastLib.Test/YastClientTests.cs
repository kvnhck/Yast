using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using NUnit.Framework;
using YastLib.Auth;
using YastLib.Common;
using YastLib.Report;

namespace YastLib.Test
{
    [TestFixture]
    public class YastClientTests
    {
        private const string _baseUri = "http://www.yast.com";

        private YastClient _yast;
        private string _user;
        private string _password;

        [SetUp]
        public void SetUp()
        {
            _user = ConfigurationManager.AppSettings["Yast.User"];
            _password = ConfigurationManager.AppSettings["Yast.Password"];
            _yast = new YastClient(_baseUri);
        }

        private LoginResponse Login()
        {
            return _yast.Login(_user, _password);
        }

        [Test]
        public void Login_ShouldRespondWithStatusSuccessAndHash()
        {
            var response = Login();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotEmpty(response.Hash);
        }

        [Test]
        public void GetUserInfo_ShouldRespondWithAValidUserInfoObject()
        {
            var login = Login();

            var userInfo = _yast.GetUserInfo(
                _user,
                login.Hash);

            Assert.IsNotNull(userInfo);
            Assert.IsNotEmpty(userInfo.Name);
            Assert.Greater(userInfo.TimeCreated, new DateTime(1970, 1, 1));
            Assert.Greater(userInfo.Id, 0);
        }

        [Test]
        public void GetReport_ShouldRespondWithReportIdAndReportHash()
        {
            var login = Login();

            var report = _yast.GetReport(
                _user,
                login.Hash,
                ReportFormat.Pdf);

            Assert.IsNotNull(report);
            Assert.Greater(report.ReportId, 0);
            Assert.IsNotEmpty(report.ReportHash);

            Console.WriteLine(
                report.GetDownloadUrl(
                    new Uri(_baseUri),
                    _user,
                    login.Hash));
        }

        [Test]
        public void GetReportWithExtraOptions_ShouldRespondWithReportIdAndReportHash()
        {
            var login = Login();

            var report = _yast.GetReport(
                _user,
                login.Hash,
                ReportFormat.Pdf,
                timeFrom: new DateTime(DateTime.Now.Year, 1, 1),
                timeTo: DateTime.Now.AddDays(1).Date,
                groupBy: new List<string> { ReportGrouping.Week });

            Assert.IsNotNull(report);
            Assert.Greater(report.ReportId, 0);
            Assert.IsNotEmpty(report.ReportHash);

            Console.WriteLine(
                report.GetDownloadUrl(
                    new Uri(_baseUri),
                    _user,
                    login.Hash));
        }
    }
}
