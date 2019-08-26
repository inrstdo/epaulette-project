using System;
using System.Collections.Generic;
using epaulette_data.epaulette_database_model;

namespace epaulette_data.epaulette_interface
{
  public interface IEpauletteGettor
  {
    bool OpenConnection(string connection);

    Author GetAuthor();

    Post GetLatestPost();
    Post GetOldestPost();
    Post GetNextPost(DateTime date);
    Post GetPrevPost(DateTime date);
    Post GetPost(int postId);

    IEnumerable<Tag> GetPostTags(int postid);

    PostContent GetPostContent(int postId);

    EpauletteContent GetEpauletteContent(int postId);
  }
}
