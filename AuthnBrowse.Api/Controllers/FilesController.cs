using System;
using System.Collections.Generic;
using AuthnBrowse.Api.Data;
using AuthnBrowse.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthnBrowse.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;
        private readonly IFileSystemService _fileSystemService;

        public FilesController(ILogger<FilesController> logger, IFileSystemService fileSystemService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        }

        [HttpGet]
        [Route("{path?}")]
        public IEnumerable<FileInformation> Get(string path)
        {
            return _fileSystemService.GetFiles(path);
        }
    }
}