using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthnBrowse.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<FilesController> _logger;

        public FilesController(ILogger<FilesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<FileInformation> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new FileInformation
                {
                    FileName = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}