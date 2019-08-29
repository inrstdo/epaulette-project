using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using epaulette_data.epaulette_database_model;
using epaulette_data.epaulette_interface;

namespace epaulette_read_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IEpauletteGettor _gettor;

        public PostsController(/*ILogger<PostsController> logger, */IEpauletteGettor gettor)
        {
            _gettor = gettor;
        }

        [HttpGet]
        public Post GetLatest()
        {
          return _gettor.GetLatestPost();
        }
    }
}
