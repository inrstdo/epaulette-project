using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using epaulette_data.epaulette_database_model;
using epaulette_data.epaulette_interface;

namespace epaulette_data.epaulette_json
{
  public class EpauletteJsonGettor : IEpauletteGettor
  {
    private bool isConnected;
    private string fileName;
    private JObject parsedObject;

    private string GetPropertyObject(IEnumerable<string> properties)
    {
      if(parsedObject == null)
      {
        throw new InvalidOperationException();
      }

      JToken propertyObject = parsedObject;

      foreach(var property in properties)
      {
        propertyObject = propertyObject[property];
      }

      return propertyObject.ToString();
    }

    private T ConvertSingleObject<T>(IEnumerable<string> properties)
    {
       var propertyObject = GetPropertyObject(properties);

       return JsonConvert.DeserializeObject<T>(propertyObject.ToString());
    }

    private List<T> ConvertListOfObjects<T>(IEnumerable<string> properties)
    {
      var propertyObject = GetPropertyObject(properties);

      return JsonConvert.DeserializeObject<List<T>>(propertyObject.ToString());
    }

    private List<Post> GetPosts()
    {
      return ConvertListOfObjects<Post>(new string[] { "posts" });
    }

    public bool IsConnected
    {
      get
      {
        return isConnected;
      }
    }

    public bool OpenConnection(string connection)
    {
      fileName = connection;

      if(!File.Exists(fileName)) 
      {
        CloseConnection();
        return false;
      }
      
      parsedObject = JObject.Parse(File.ReadAllText(fileName));
      isConnected = true;

      return true;
    }

    public bool CloseConnection()
    {
      isConnected = false;
      fileName = null;
      parsedObject = null;

      return true;
    }

    public Author GetAuthor()
    {
      return ConvertSingleObject<Author>(new string[] { "author" });
    }

    public Post GetLatestPost()
    {
      var postObjects = GetPosts();

      return postObjects.OrderBy(x => x.Date).Last();
    }
  
    public Post GetOldestPost()
    {
      var postObjects = GetPosts();

      return postObjects.OrderBy(x => x.Date).First();
    }

    public Post GetNextPost(DateTime date)
    {
      var postObjects = GetPosts();

      return postObjects.Where(x => x.Date > date).OrderBy(x => x.Date).First();
    }

    public Post GetPrevPost(DateTime date)
    {
      var postObjects = GetPosts();

      return postObjects.Where(x => x.Date < date).OrderBy(x => x.Date).Last();
    }

    public Post GetNextPost(int postId)
    {
      var postObjects = GetPosts();
      var currentPost = postObjects.SingleOrDefault(x => x.PostId == postId);

      if (currentPost == null)
      {
        throw new InvalidOperationException("Cannot retrieve next post after an invalid postId");
      }

      return postObjects.Where(x => x.Date > currentPost.Date).OrderBy(x => x.Date).First();
    }

    public Post GetPrevPost(int postId)
    {
      var postObjects = GetPosts();
      var currentPost = postObjects.SingleOrDefault(x => x.PostId == postId);

      if (currentPost == null)
      {
        throw new InvalidOperationException("Cannot retrieve previous post prior to an invalid postId");
      }

      return postObjects.Where(x => x.Date < currentPost.Date).OrderBy(x => x.Date).Last();
    }

    public Post GetPost(int postId)
    {
      var postObjects = GetPosts();
      return postObjects.SingleOrDefault(x => x.PostId == postId);
    }

    public IEnumerable<Tag> GetPostTags(int postId)
    {
      var currentPost = GetPost(postId);

      if(currentPost == null)
      {
        throw new InvalidOperationException("Invalid postId");
      }

      var postTagsObjects = ConvertListOfObjects<PostTag>(new string[] { "postTags" });
      var currentPostTagIds = new HashSet<int>(postTagsObjects.Where(x => x.PostId == currentPost.PostId).Select(x => x.TagId));
      var tagsObjects = ConvertListOfObjects<Tag>(new string[] { "tags" });
      
      return tagsObjects.Where(x => currentPostTagIds.Contains(x.TagId)).ToList();
    }

    public PostContent GetPostContent(int postId)
    {
      var currentPost = GetPost(postId);

      if(currentPost == null)
      {
        throw new InvalidOperationException("Invalid postId");
      }

      var postContentObjects = ConvertListOfObjects<PostContent>(new string[] { "postContent" });

      return postContentObjects.SingleOrDefault(x => x.PostId == currentPost.PostId);
    }

    public EpauletteContent GetEpauletteContent(int postId)
    {
      var currentPost = GetPost(postId);

      if(currentPost == null)
      {
        throw new InvalidOperationException("Invalid postId");
      }

      var epauletteContentObjects = ConvertListOfObjects<EpauletteContent>(new string[] { "epauletteContent" });

      return epauletteContentObjects.SingleOrDefault(x => x.PostId == currentPost.PostId);
    }
  }
}