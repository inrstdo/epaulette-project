using System;
using System.Collections.Generic;
using epaulette_data.epaulette_database_model;

namespace epaulette_data.epaulette_interface
{
  public interface IEpauletteGettor
  {
    bool IsConnected
    {
      get;
    }

    bool OpenConnection(string connection);
    bool CloseConnection();

    Author GetAuthor();

    Post GetLatestPost();
    Post GetOldestPost();
    Post GetNextPost(DateTime date);
    Post GetPrevPost(DateTime date);
    Post GetNextPost(int postId);
    Post GetPrevPost(int postId);
    Post GetPost(int postId);

    IEnumerable<Tag> GetPostTags(int postId);

    PostContent GetPostContent(int postId);

    EpauletteContent GetEpauletteContent(int postId);
  }
}
