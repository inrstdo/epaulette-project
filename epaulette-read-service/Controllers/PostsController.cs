using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using epaulette_data.epaulette_interface;
using epaulette_read_service.Controllers.BaseClass;
using epaulette_read_service.ViewModel;

namespace epaulette_read_service.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PostsController : EpauletteControllerBase
  {
    public PostsController(
      IOptions<AppSettings> appSettings,
      IMapper mapper,
      /* ILogger<PostsController> logger, */
      IEpauletteGettor gettor) : 
    base(
      appSettings,
      mapper,
      /* logger, */
      gettor)
    {
    }

    [HttpGet("Count")]
    public ActionResult<int> GetCount()
    {
      _gettor.OpenConnection(_appSettings.StorageConnectionString);
      
      var result = _gettor.GetAllPostIds().Count();

      _gettor.CloseConnection();

      return result;
    }

    [HttpGet("Latest")]
    public ActionResult<ViewPostNeighborsModel> GetLatest()
    {
      _gettor.OpenConnection(_appSettings.StorageConnectionString);
      
      var current = _gettor.GetLatestPost();
      var next = _gettor.GetNextPost(current.PostId);
      var prev = _gettor.GetPrevPost(current.PostId);

      _gettor.CloseConnection();

      var result = new ViewPostNeighborsModel()
      {
        Current = _mapper.Map<Post>(current),
        Next = _mapper.Map<Post>(next),
        Prev = _mapper.Map<Post>(prev),
      };

      return result;
    }

    [HttpGet("Post/{postId}")]
    public ActionResult<ViewPostNeighborsModel> GetPost(int postId)
    {
      _gettor.OpenConnection(_appSettings.StorageConnectionString);
      
      var current = _gettor.GetPost(postId);
      var next = _gettor.GetNextPost(current.PostId);
      var prev = _gettor.GetPrevPost(current.PostId);

      _gettor.CloseConnection();

      var result = new ViewPostNeighborsModel()
      {
        Current = _mapper.Map<Post>(current),
        Next = _mapper.Map<Post>(next),
        Prev = _mapper.Map<Post>(prev),
      };

      return result;
    }
  }
}
