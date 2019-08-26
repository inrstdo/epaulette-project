using System;

namespace epaulette_data.epaulette_database_model
{
    public class Post
    {
      public int PostId
      {
        get;
        set;
      }

      public DateTime Date
      {
        get;
        set;
      }

      public int TypeId
      {
        get;
        set;
      }
    }
}
