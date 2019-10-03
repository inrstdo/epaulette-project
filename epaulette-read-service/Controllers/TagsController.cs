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
    private readonly int _blurbLength;

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
      _blurbLength = appSettings.Value.TagSearchContentBlurbLength;
    }

    private string ConvertToBlurb(string content)
    {
      return content.Length <= _blurbLength ? content : $"{content.Substring(0, _blurbLength).TrimEnd()}...";
    }

    private ViewTagSearchModel[] TagSearchById(int tagId, IEpauletteGettor gettor)
    {
      var tagSearch = gettor.GetPostsWithTag(tagId);

      var result = tagSearch.Select(x =>
        new ViewTagSearchModel()
        {
          Post = _mapper.Map<Post>(x.Item1),
          Title = x.Item2.Title,
          ContentBlurb = ConvertToBlurb(x.Item2.Content)
        }).ToArray();

      return result;
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

    [HttpGet("Search/Id/{tagId}")]
    public ActionResult<ViewTagSearchModel[]> GetSearchById(int tagId)
    {
      _gettor.OpenConnection(_appSettings.StorageConnectionString);
      
      var result = TagSearchById(tagId, _gettor);

      _gettor.CloseConnection();

      return result;
    }

    [HttpGet("Search/Name/{tagName}")]
    public ActionResult<ViewTagSearchModel[]> GetSearchByName(string tagName)
    {
      _gettor.OpenConnection(_appSettings.StorageConnectionString);
      
      var tag = _gettor.GetTagByName(tagName);
      var result = tag != null ? TagSearchById(tag.TagId, _gettor) : new ViewTagSearchModel[] {};

      _gettor.CloseConnection();

      return result;
    }
  }
}
