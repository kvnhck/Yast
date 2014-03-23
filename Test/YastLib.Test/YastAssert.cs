using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using YastLib.Common;
using YastLib.Data;
using YastLib.Meta;
using YastLib.Report;
using YastLib.User;

namespace YastLib.Test
{
    static class YastAssert
    {
        public static void AssertResponse(YastResponse response)
        {
            Assert.IsNotNull(response);
            Assert.AreEqual(Status.Success, response.Status);
        }

        public static void AssertResponse(GetUserInfoResponse response)
        {
            AssertResponse((YastResponse) response);

            Assert.IsNotEmpty(response.Name);
            Assert.Greater(response.TimeCreated, new DateTime(1970, 1, 1));
            Assert.Greater(response.Id, 0);
        }

        public static void AssertResponse(GetUserSettingsResponse response)
        {
            AssertResponse((YastResponse) response);

            CollectionAssert.IsNotEmpty(response.Settings);
        }

        public static void AssertResponse(GetRecordsResponse response)
        {
            AssertResponse((YastResponse) response);

            AssertCollection(response.Records);
        }

        public static void AssertResponse(GetFoldersResponse response)
        {
            AssertResponse((YastResponse) response);

            AssertCollection(response.Folders);
        }


        public static void AssertResponse(GetProjectsResponse response)
        {
            AssertResponse((YastResponse) response);

            AssertCollection(response.Projects);
        }

        public static void AssertResponse(GetReportResponse response)
        {
            AssertResponse((YastResponse) response);

            Assert.Greater(response.ReportId, 0);
            Assert.IsNotEmpty(response.ReportHash);
        }

        public static void AssertResponse(GetRecordTypesResponse response)
        {
            AssertResponse((YastResponse) response);

            AssertCollection(response.RecordTypes);
        }

        //

        private static void AssertCollection(IEnumerable<YastElement> collection)
        {
            CollectionAssert.IsNotEmpty(collection);
            Assert.IsTrue(collection.All(r => r.Id > 0));
        }
    }
}
