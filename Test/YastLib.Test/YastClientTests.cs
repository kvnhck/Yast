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

        private IYastClient _yast;
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

            YastAssert.AssertResponse(response);

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

            var response = _yast.GetUserInfo(token);

            YastAssert.AssertResponse(response);
            
            Console.WriteLine("UserInfo:");
            Console.WriteLine("Id: {0}", response.Id);
            Console.WriteLine("Name: {0}", response.Name);
            Console.WriteLine("Created: {0}", response.TimeCreated);
            Console.WriteLine("Status: {0}", response.Status);
            Console.WriteLine("ValidSubscription: {0}", response.ValidSubscription);
        }

        [Test]
        public void GetUserSettings_ShouldRespondWithUserSettings()
        {
            var token = Login();

            var response = _yast.GetUserSettings(token);

            YastAssert.AssertResponse(response);

            Console.WriteLine("Settings:");
            Console.WriteLine(string.Join(Environment.NewLine, response.Settings.Select(s => s.Key + ": " + s.Value)));
        }

        [Test]
        public void GetRecords_ShouldReturnRecords()
        {
            var token = Login();

            var response = _yast.GetRecords(token);

            YastAssert.AssertResponse(response);
            
            Console.WriteLine("Records:");
            Console.WriteLine(string.Join(Environment.NewLine,
                response.Records.OfType<YastWorkRecord>().Select(r => string.Format("{0}: {1} - {2} ({3})", r.Id, r.StartTime, r.EndTime, r.GetType().Name))));
        }

        [Test]
        public void GetFolders_ShouldReturnFolders()
        {
            var token = Login();

            var response = _yast.GetFolders(token);

            YastAssert.AssertResponse(response);
            
            Console.WriteLine("Folders:");
            Console.WriteLine(string.Join(Environment.NewLine,
                response.Folders.Select(f => string.Format("{0}: {1} (parent: {2})", f.Id, f.Name, f.ParentId))));
        }

        [Test]
        public void GetProjects_ShouldReturnProjects()
        {
            var token = Login();

            var response = _yast.GetProjects(token);

            YastAssert.AssertResponse(response);

            Console.WriteLine("Projects:");
            Console.WriteLine(string.Join(Environment.NewLine,
                response.Projects.Select(p => string.Format("{0}: {1} (parent: {2})", p.Id, p.Name, p.ParentId))));
        }

        [Test]
        public void GetReport_ShouldRespondWithReportIdAndReportHash()
        {
            var token = Login();

            var response = _yast.GetReport(token, ReportFormat.Pdf);

            YastAssert.AssertResponse(response);

            Console.WriteLine(
                response.GetDownloadUrl(new Uri(BaseUri), token));
        }

        [Test]
        public void GetReportWithExtraOptions_ShouldRespondWithReportIdAndReportHash()
        {
            var token = Login();

            var response = _yast.GetReport(
                token,
                ReportFormat.Pdf,
                timeFrom: new DateTime(DateTime.Now.Year, 1, 1),
                timeTo: DateTime.Now.AddDays(1).Date,
                groupBy: new List<string> { ReportGrouping.Week });

            YastAssert.AssertResponse(response);

            Console.WriteLine(
                response.GetDownloadUrl(new Uri(BaseUri), token));
        }

        [Test]
        public void GetRecordTypes_ShouldReturnRecordTypes()
        {
            var token = Login();

            var response = _yast.GetRecordTypes(token);

            YastAssert.AssertResponse(response);

            Console.WriteLine("RecordTypes:");
            foreach(var rt in response.RecordTypes)
                Console.WriteLine("{0}: {1} ({2})", rt.Id, rt.Name,
                    string.Join(", ", rt.VariableTypes.Select(vt => vt.Name)));
        }

        [Test]
        public void AddWorkRecord_ShouldCreateANewWorkRecord()
        {
            var token = Login();

            var projects = _yast.GetProjects(token);
            var projectId = projects.Projects.First().Id;

            var response = _yast.AddRecords(token,
                new YastWorkRecord
                {
                    StartTime = DateTime.Now.AddHours(-1),
                    EndTime = DateTime.Now.AddHours(1),
                    ProjectId = projectId,
                    Comment = "Dit is een test comment"
                });

            YastAssert.AssertResponse(response);
        }

        [Test]
        public void AddProjectRecord_ShouldCreateANewProjectRecord()
        {
            var token = Login();

            var response = _yast.AddRecords(token,
                new YastProject
                {
                    ParentId = 0,
                    Name = "Test Project",
                    PrimaryColor = "#DDDDDD",
                    Description = "Test Project Description",
                    Privileges = YastPrivilege.Owner
                });

            YastAssert.AssertResponse(response);
        }

        [Test]
        public void AddFolderRecord_ShouldCreateANewFolderRecord()
        {
            var token = Login();

            var response = _yast.AddRecords(token,
                new YastFolder
                {
                    Name = "Test Folder",
                    Description = "Test Folder Description",
                    ParentId = 0,
                    PrimaryColor = "#DDDDDD",
                    Privileges = YastPrivilege.Owner
                });

            YastAssert.AssertResponse(response);
        }

        [Test]
        public void AddSubFolderRecord_ShouldCreateANewFolderRecord()
        {
            var token = Login();

            var response = _yast.AddRecords(token,
                new YastFolder
                {
                    Name = "Test Folder",
                    Description = "Test Folder Description",
                    ParentId = 0,
                    PrimaryColor = "#DDDDDD",
                    Privileges = YastPrivilege.Owner
                });

            var folderId = response.Folders.First().Id;

            response = _yast.AddRecords(token,
                new YastFolder
                {
                    Name = "Test Subfolder",
                    Description = "Test Subfolder Description",
                    ParentId = folderId,
                    PrimaryColor = "#DDDDDD",
                    Privileges = YastPrivilege.Owner
                });

            YastAssert.AssertResponse(response);
        }
    }
}
