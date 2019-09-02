using System;

namespace epaulette_read_service.ViewModel
{
    public class ViewPostNeighborsModel
    {
      public Post Current
      {
        get;
        set;
      }

      public Post Next
      {
        get;
        set;
      }

      public Post Prev
      {
        get;
        set;
      }
    }
}
