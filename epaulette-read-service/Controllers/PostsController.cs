using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using epaulette_data.epaulette_interface;
using epaulette_read_service.ViewModel;

namespace epaulette_read_service.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PostsController : ControllerBase
  {
    private readonly AppSettings _appSettings;
    private readonly IMapper _mapper;
    private readonly ILogger<PostsController> _logger;
    private readonly IEpauletteGettor _gettor;

    public PostsController(
      IOptions<AppSettings> appSettings,
      IMapper mapper,
      /*ILogger<PostsController> logger, */
      IEpauletteGettor gettor)
    {
      _appSettings = appSettings.Value;
      _mapper = mapper;
      _gettor = gettor;
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
