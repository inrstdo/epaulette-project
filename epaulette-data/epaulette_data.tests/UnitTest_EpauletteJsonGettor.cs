using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using epaulette_data.epaulette_json;

namespace epaulette_data.tests
{
  public class Test_EpauletteJsonGettor
  {
    private EpauletteJsonGettor testObject;
    private static readonly string invalidFile = "thisdoesnotexist.json";
    private static readonly string flatFile = "epaulette-database.json";
    private static readonly string embeddedFlatFile = $"epaulette_data.tests.{flatFile}";
    private static readonly string localFlatFile = Path.Combine(TestContext.CurrentContext.TestDirectory, flatFile);

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      var assembly = Assembly.GetExecutingAssembly();
      var resourceName = embeddedFlatFile;
      string resourceContents;

      using (Stream stream = assembly.GetManifestResourceStream(resourceName))
      using (StreamReader reader = new StreamReader(stream))
      {
          resourceContents = reader.ReadToEnd();
      }

      using (StreamWriter writer = new StreamWriter(localFlatFile))
      {
        writer.Write(resourceContents);
      }
    }

    [SetUp]
    public void Setup()
    {
      testObject = new EpauletteJsonGettor();
    }

    [TearDown]
    public void TearDown()
    {
      if(testObject.IsConnected)
      {
        testObject.CloseConnection();
      }

      testObject = null;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
      if(File.Exists(localFlatFile))
      {
        File.Delete(localFlatFile);
      }
    }

    [Test]
    public void Test_OpenConnection_Valid()
    {
      var connected = testObject.OpenConnection(localFlatFile);

      Assert.IsTrue(connected);
      Assert.IsTrue(testObject.IsConnected);
    }

    [Test]
    public void Test_OpenConnection_Invalid()
    {
      var connected = testObject.OpenConnection(invalidFile);

      Assert.IsFalse(connected);
      Assert.IsFalse(testObject.IsConnected);
    }

    [Test]
    public void Test_GetAuthor()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetAuthor();

      Assert.AreEqual("Iron and Rose Studios", testResult.Name);
    }

    [Test]
    public void Test_GetAllPostIds()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetAllPostIds();

      Assert.AreEqual(3, testResult.Count());
      Assert.AreEqual(1, testResult.ElementAt(0));
      Assert.AreEqual(2, testResult.ElementAt(1));
      Assert.AreEqual(3, testResult.ElementAt(2));
    }

    [Test]
    public void Test_GetLatestPost()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetLatestPost();

      Assert.AreEqual(3, testResult.PostId);
    }

    [Test]
    public void Test_GetOldestPost()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetOldestPost();

      Assert.AreEqual(1, testResult.PostId);
    }

    [Test]
    public void Test_GetNextPostByDate1()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetNextPost(DateTime.Parse("2019-08-27T00:00:00-0800"));

      Assert.AreEqual(2, testResult.PostId);
    }

    [Test]
    public void Test_GetNextPostByDate2()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetNextPost(DateTime.Parse("2019-08-30T20:33:02-0800"));

      Assert.AreEqual(3, testResult.PostId);
    }

    [Test]
    public void Test_GetNextPostByDate3()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetNextPost(DateTime.Parse("2019-10-01T00:00:00-0800"));

      Assert.IsNull(testResult);
    }

    [Test]
    public void Test_GetNextPostByDate4()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetNextPost(DateTime.Parse("2000-01-01T00:00:00-0800"));

      Assert.AreEqual(1, testResult.PostId);
    }

    [Test]
    public void Test_GetPrevPostByDate1()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPrevPost(DateTime.Parse("2019-08-27T00:00:00-0800"));

      Assert.AreEqual(1, testResult.PostId);
    }

    [Test]
    public void Test_GetPrevPostByDate2()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPrevPost(DateTime.Parse("2019-08-30T20:33:02-0800"));

      Assert.AreEqual(1, testResult.PostId);
    }

    [Test]
    public void Test_GetPrevPostByDate3()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPrevPost(DateTime.Parse("2019-10-01T00:00:00-0800"));

      Assert.AreEqual(3, testResult.PostId);
    }

    [Test]
    public void Test_GetPrevPostByDate4()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPrevPost(DateTime.Parse("2000-01-01T00:00:00-0800"));

      Assert.IsNull(testResult);
    }

    [Test]
    public void Test_GetNextPostById1()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetNextPost(1);

      Assert.AreEqual(2, testResult.PostId);
    }

    [Test]
    public void Test_GetNextPostById2()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetNextPost(3);

      Assert.IsNull(testResult);
    }

    [Test]
    public void Test_GetNextPostById3()
    {
      testObject.OpenConnection(localFlatFile);

      Assert.Throws<ArgumentException>(() => {
        var testResult = testObject.GetNextPost(0);
      });
    }

    [Test]
    public void Test_GetPrevPostById1()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPrevPost(2);

      Assert.AreEqual(1, testResult.PostId);
    }

    [Test]
    public void Test_GetPrevPostById2()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPrevPost(1);

      Assert.IsNull(testResult);
    }

    [Test]
    public void Test_GetPrevPostById3()
    {
      testObject.OpenConnection(localFlatFile);

      Assert.Throws<ArgumentException>(() => {
        var testResult = testObject.GetPrevPost(0);
      });
    }

    [Test]
    public void Test_GetPost1()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPost(1);

      Assert.AreEqual(1, testResult.PostId);
      Assert.AreEqual(DateTime.Parse("2019-08-26T19:44:02-0800"), testResult.Date);
      Assert.AreEqual(1, testResult.TypeId);
    }

    [Test]
    public void Test_GetPost2()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPost(2);

      Assert.AreEqual(2, testResult.PostId);
      Assert.AreEqual(DateTime.Parse("2019-08-30T20:33:02-0800"), testResult.Date);
      Assert.AreEqual(2, testResult.TypeId);
    }

    [Test]
    public void Test_GetPost3()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPost(0);

      Assert.IsNull(testResult);
    }

    [Test]
    public void Test_GetPostTags()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPostTags(3);

      Assert.AreEqual(2, testResult.Count());
      Assert.IsTrue(testResult.Any(x => x.Name == "good"));
      Assert.IsTrue(testResult.Any(x => x.Name == "best"));
    }

    [Test]
    public void Test_GetPostContent()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPostContent(2);

      Assert.AreEqual(2, testResult.PostId);
      Assert.AreEqual("This Is A Ribbon Test", testResult.Title);
      Assert.AreEqual("Still feelin' good", testResult.Content);
    }

    [Test]
    public void Test_GetEpauletteContent()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetEpauletteContent(1);

      Assert.AreEqual(1, testResult.PostId);
      Assert.AreEqual(1, testResult.ExternalHostId);
      Assert.AreEqual("something", testResult.ExternalPostId);
    }

    [Test]
    public void Test_GetTagCounts1()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetTagCounts();

      Assert.IsTrue(testResult.ElementAt(0).Item1.TagId == 1 && testResult.ElementAt(0).Item2 == 3);
      Assert.IsTrue(testResult.Skip(1).Any(x => x.Item1.TagId == 2 && x.Item2 == 1));
      Assert.IsTrue(testResult.Skip(1).Any(x => x.Item1.TagId == 3 && x.Item2 == 1));
    }

    [Test]
    public void Test_GetTagCounts2()
    {
      var maxListSize = 2;

      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetTagCounts(maxListSize);

      Assert.AreEqual(maxListSize, testResult.Count());
      Assert.AreEqual(3, testResult.ElementAt(0).Item2);
      Assert.AreEqual(1, testResult.ElementAt(1).Item2);
    }

    [Test]
    public void Test_GetPostsWithTag()
    {
      testObject.OpenConnection(localFlatFile);

      var testResult = testObject.GetPostsWithTag(1);

      Assert.AreEqual(3, testResult.Count());
      Assert.AreEqual(3, testResult.ElementAt(0).Item1.PostId);
      Assert.AreEqual(2, testResult.ElementAt(1).Item1.PostId);
      Assert.AreEqual(1, testResult.ElementAt(2).Item1.PostId);
    }
  }
}