using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using epaulette_data.epaulette_database_model;
using epaulette_data.epaulette_interface;

namespace epaulette_write_service.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PostsController : ControllerBase
  {
    private readonly AppSettings _appSettings;
    private readonly ILogger<PostsController> _logger;
    private readonly IEpauletteGettor _gettor;

    public PostsController(
      IOptions<AppSettings> appSettings,
      /*ILogger<PostsController> logger, */
      IEpauletteGettor gettor)
    {
      _appSettings = appSettings.Value;
      _gettor = gettor;
    }

    [HttpPost()]
    public ActionResult<Post> PostLatest()
    {
      throw new NotImplementedException();
    }
  }
}
