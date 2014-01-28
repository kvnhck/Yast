using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using YastLib.Auth;
using YastLib.Common;
using YastLib.Report;

namespace YastLib.Test
{
    [TestFixture]
    public class YastClientTests
    {
        private const string BaseUri = "http://www.yast.com";

        private YastClient _yast;
        private string _user;
        private string _password;

        [SetUp]
        public void SetUp()
        {
            _user = ConfigurationManager.AppSettings["Yast.User"];
            _password = ConfigurationManager.AppSettings["Yast.Password"];
            _yast = new YastClient(BaseUri);
        }

        private LoginResponse Login(int? requestId = null)
        {
            return _yast.Login(_user, _password, requestId);
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
        public void LoginWithRequestId_ShouldRespondWithTheSameRequestId()
        {
            var response = Login(123);

            Assert.AreEqual(123, response.RequestId);
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
            Assert.AreEqual(false, userInfo.ValidSubscription);
        }

        [Test]
        public void GetRecords_ShouldReturnRecords()
        {
            var login = Login();

            var response = _yast.GetRecords(
                _user,
                login.Hash);

            Assert.IsNotNull(response);
            CollectionAssert.IsNotEmpty(response.Records);
            Assert.IsTrue(response.Records.All(r => r.Id > 0));
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
                    new Uri(BaseUri),
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
                    new Uri(BaseUri),
                    _user,
                    login.Hash));
        }
    }
}
