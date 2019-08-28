using NUnit.Framework;
using epaulette_data.epaulette_json;

namespace epaulette_data.tests
{
  public class Test_EpauletteJsonGettor
  {
    private EpauletteJsonGettor testObject;
    private readonly string flatfile = "../flat-file/epaulette-database.json";
    private readonly string invalidFile = "thisdoesnotexist.json";

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

    [Test]
    public void Test_OpenConnection_Valid()
    {
      var connected = testObject.OpenConnection(flatfile);

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
      Assert.Pass();
    }
  }
}