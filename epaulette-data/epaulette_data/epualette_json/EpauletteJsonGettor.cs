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

    public IEnumerable<int> GetAllPostIds()
    {
      var postObjects = GetPosts();

      return postObjects.Select(x => x.PostId).ToList();
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

      return postObjects.Where(x => x.Date > date).OrderBy(x => x.Date).FirstOrDefault();
    }

    public Post GetPrevPost(DateTime date)
    {
      var postObjects = GetPosts();

      return postObjects.Where(x => x.Date < date).OrderBy(x => x.Date).LastOrDefault();
    }

    public Post GetNextPost(int postId)
    {
      var postObjects = GetPosts();
      var currentPost = postObjects.SingleOrDefault(x => x.PostId == postId);

      if (currentPost == null)
      {
        throw new ArgumentException("Cannot retrieve next post after an invalid postId");
      }

      return postObjects.Where(x => x.Date > currentPost.Date).OrderBy(x => x.Date).FirstOrDefault();
    }

    public Post GetPrevPost(int postId)
    {
      var postObjects = GetPosts();
      var currentPost = postObjects.SingleOrDefault(x => x.PostId == postId);

      if (currentPost == null)
      {
        throw new ArgumentException("Cannot retrieve previous post prior to an invalid postId");
      }

      return postObjects.Where(x => x.Date < currentPost.Date).OrderBy(x => x.Date).LastOrDefault();
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
        throw new ArgumentException("Invalid postId");
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
        throw new ArgumentException("Invalid postId");
      }

      var postContentObjects = ConvertListOfObjects<PostContent>(new string[] { "postContent" });

      return postContentObjects.SingleOrDefault(x => x.PostId == currentPost.PostId);
    }

    public EpauletteContent GetEpauletteContent(int postId)
    {
      var currentPost = GetPost(postId);

      if(currentPost == null)
      {
        throw new ArgumentException("Invalid postId");
      }

      var epauletteContentObjects = ConvertListOfObjects<EpauletteContent>(new string[] { "epauletteContent" });

      return epauletteContentObjects.SingleOrDefault(x => x.PostId == currentPost.PostId);
    }

    public Tag GetTagByName(string tagName)
    {
      var tagObjects = ConvertListOfObjects<Tag>(new string [] { "tags" });

      return tagObjects.FirstOrDefault(x => x.Name == tagName);
    }

    public IEnumerable<Tuple<Tag, int>> GetTagCounts(int? maxListSize = null)
    {
      var tagObjects = ConvertListOfObjects<Tag>(new string[] { "tags" });
      var postTagObjects = ConvertListOfObjects<PostTag>(new string[] { "postTags" });

      var tagCountObjects = tagObjects.Select(x => new Tuple<Tag, int>(x, postTagObjects.Count(y => y.TagId == x.TagId))).OrderByDescending(x => x.Item2).ToList();

      return maxListSize.HasValue ? tagCountObjects.Take(maxListSize.Value) : tagCountObjects;
    }

    public IEnumerable<Tuple<Post, PostContent>> GetPostsWithTag(int tagId)
    {
      var postObjects = ConvertListOfObjects<Post>(new string[] { "posts" });
      var postContentObjects = ConvertListOfObjects<PostContent>(new string[] { "postContent" });
      var postTagObjects = ConvertListOfObjects<PostTag>(new string[] { "postTags" });

      var relevantPostIds = new HashSet<int>(postTagObjects.Where(x => x.TagId == tagId).Select(x => x.PostId));
      var relevantPostObjects = postObjects.Where(x => relevantPostIds.Contains(x.PostId)).ToList();
      var relevantPostContentObjects = postContentObjects.Where(x => relevantPostIds.Contains(x.PostId)).ToList();

      return relevantPostObjects.Join(relevantPostContentObjects, x => x.PostId, y => y.PostId, (x, y) => new Tuple<Post, PostContent>(x, y)).OrderByDescending(x => x.Item1.Date).ToList();
    }
  }
}
