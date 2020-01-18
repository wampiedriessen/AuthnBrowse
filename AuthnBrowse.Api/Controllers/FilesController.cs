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
        [Route("")]
        public IEnumerable<FileInformation> Get()
        {
            return _fileSystemService.GetRootFiles();
        }

        [HttpGet]
        [Route("/dir/{fileId:guid}")]
        public IActionResult Get(Guid fileId)
        {
            try
            {
                return Ok(_fileSystemService.GetDirectory(fileId));
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (FileNotADirectoryException)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/file/{fileId:guid}")]
        public IActionResult Download(Guid fileId)
        {
            try
            {
                using (var file = _fileSystemService.GetDownload(fileId))
                {
                    _logger.Log(LogLevel.Information, "File downloaded", fileId);
                    return File(file, "application/download");
                }
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (FileNotARegularFileException)
            {
                return BadRequest();
            }
        }
    }
}