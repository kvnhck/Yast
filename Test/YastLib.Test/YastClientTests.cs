using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using YastLib.Common;
using YastLib.Data;
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

        private YastAuthToken Login(int? requestId = null)
        {
            var response = _yast.Login(_user, _password, requestId);

            return new YastAuthToken(_user, response.Hash);
        }

        [Test]
        public void Login_ShouldRespondWithStatusSuccessAndHash()
        {
            var response = _yast.Login(_user, _password);

            Assert.IsNotNull(response);
            Assert.AreEqual(Status.Success, response.Status);
            Assert.IsNotEmpty(response.Hash);

            Console.WriteLine("Login:");
            Console.WriteLine("User: {0}", _user);
            Console.WriteLine("Hash: {0}", response.Hash);
        }

        [Test]
        public void LoginWithRequestId_ShouldRespondWithTheSameRequestId()
        {
            var response = _yast.Login(_user, _password, 123);

            Assert.AreEqual(123, response.RequestId);
        }

        [Test]
        public void GetUserInfo_ShouldRespondWithAValidUserInfoObject()
        {
            var token = Login();

            var userInfo = _yast.GetUserInfo(token);

            Assert.IsNotNull(userInfo);
            Assert.IsNotEmpty(userInfo.Name);
            Assert.Greater(userInfo.TimeCreated, new DateTime(1970, 1, 1));
            Assert.Greater(userInfo.Id, 0);

            Console.WriteLine("UserInfo:");
            Console.WriteLine("Id: {0}", userInfo.Id);
            Console.WriteLine("Name: {0}", userInfo.Name);
            Console.WriteLine("Created: {0}", userInfo.TimeCreated);
            Console.WriteLine("Status: {0}", userInfo.Status);
            Console.WriteLine("ValidSubscription: {0}", userInfo.ValidSubscription);
        }

        [Test]
        public void GetRecords_ShouldReturnRecords()
        {
            var token = Login();

            var response = _yast.GetRecords(token);

            Assert.IsNotNull(response);
            CollectionAssert.IsNotEmpty(response.Records);
            Assert.IsTrue(response.Records.All(r => r.Id > 0));

            Console.WriteLine("Records:");
            Console.WriteLine(string.Join(Environment.NewLine,
                response.Records.OfType<WorkRecord>().Select(r => string.Format("{0}: {1} - {2} ({3})", r.Id, r.StartTime, r.EndTime, r.GetType().Name))));
        }

        [Test]
        public void GetFolders_ShouldReturnFolders()
        {
            var token = Login();

            var response = _yast.GetFolders(token);

            Assert.IsNotNull(response);
            CollectionAssert.IsNotEmpty(response.Folders);
            Console.WriteLine("Folders:");
            Console.WriteLine(string.Join(Environment.NewLine,
                response.Folders.Select(f => string.Format("{0}: {1} (parent: {2})", f.Id, f.Name, f.ParentId))));
        }

        [Test]
        public void GetProjects_ShouldReturnProjects()
        {
            var token = Login();

            var response = _yast.GetProjects(token);

            Assert.IsNotNull(response);
            CollectionAssert.IsNotEmpty(response.Projects);
            Assert.IsTrue(response.Projects.All(p => p.Id > 0));

            Console.WriteLine("Projects:");
            Console.WriteLine(string.Join(Environment.NewLine,
                response.Projects.Select(p => string.Format("{0}: {1} (parent: {2})", p.Id, p.Name, p.ParentId))));
        }

        [Test]
        public void GetReport_ShouldRespondWithReportIdAndReportHash()
        {
            var token = Login();

            var report = _yast.GetReport(token, ReportFormat.Pdf);

            Assert.IsNotNull(report);
            Assert.Greater(report.ReportId, 0);
            Assert.IsNotEmpty(report.ReportHash);

            Console.WriteLine(
                report.GetDownloadUrl(new Uri(BaseUri), token));
        }

        [Test]
        public void GetReportWithExtraOptions_ShouldRespondWithReportIdAndReportHash()
        {
            var token = Login();

            var report = _yast.GetReport(
                token,
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
                    token));
        }

        [Test]
        public void GetRecordTypes_ShouldReturnRecordTypes()
        {
            var token = Login();

            var response = _yast.GetRecordTypes(token);

            Assert.IsNotNull(response);
            CollectionAssert.IsNotEmpty(response.RecordTypes);
            Assert.IsTrue(response.RecordTypes.All(rt => rt.Id > 0));

            Console.WriteLine("RecordTypes:");
            foreach(var rt in response.RecordTypes)
                Console.WriteLine("{0}: {1} ({2})", rt.Id, rt.Name,
                    string.Join(", ", rt.VariableTypes.Select(vt => vt.Name)));
        }
    }
}
