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

namespace epaulette_read_service.Controllers.BaseClass
{
  public abstract class EpauletteControllerBase : ControllerBase
  {
    protected readonly AppSettings _appSettings;
    protected readonly IMapper _mapper;
    protected readonly ILogger<PostsController> _logger;
    protected readonly IEpauletteGettor _gettor;

    public EpauletteControllerBase(
      IOptions<AppSettings> appSettings,
      IMapper mapper,
      /*ILogger<PostsController> logger, */
      IEpauletteGettor gettor)
    {
      _appSettings = appSettings.Value;
      _mapper = mapper;
      _gettor = gettor;
    }
  }
}
