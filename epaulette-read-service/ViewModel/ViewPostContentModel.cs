using System;

namespace epaulette_read_service.ViewModel
{
    public class ViewPostContentModel
    {
      public PostContent PostContent
      {
        get;
        set;
      }

      public EpauletteContent EpauletteContent
      {
        get;
        set;
      }

      public Tag[] Tags
      {
        get;
        set;
      }
    }
}
