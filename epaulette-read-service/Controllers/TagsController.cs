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
  public class TagsController : EpauletteControllerBase
  {
    private const int _blurbLength = 50;

    public TagsController(
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

    [HttpGet("Counts")]
    public ActionResult<ViewTagCountsModel[]> GetTagCounts()
    {
      _gettor.OpenConnection(_appSettings.StorageConnectionString);
      
      var tagCounts = _gettor.GetTagCounts();

      _gettor.CloseConnection();

      var result = tagCounts.Select(x =>
        new ViewTagCountsModel()
        {
          Tag = _mapper.Map<Tag>(x.Item1),
          Count = x.Item2
        }).ToArray();

      return result;
    }

    [HttpGet("Search/{tagId}")]
    public ActionResult<ViewTagSearchModel[]> GetSearch(int tagId)
    {
      _gettor.OpenConnection(_appSettings.StorageConnectionString);
      
      var tagSearch = _gettor.GetPostsWithTag(tagId);

      _gettor.CloseConnection();

      var result = tagSearch.Select(x =>
        new ViewTagSearchModel()
        {
          Post = _mapper.Map<Post>(x.Item1),
          Title = x.Item2.Title,
          ContentBlurb = x.Item2.Content.Length > _blurbLength ? $"{x.Item2.Content.Substring(0, 50)} ..." : x.Item2.Content
        }).ToArray();

      return result;
    }
  }
}
