using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gcp.Models;
using gcp_demo.Storage;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace gcp.Controllers
{
    public class StorageController : Controller
    {
        private readonly ILogger<StorageController> _logger;
        private readonly IStorageWrapper _storage;     
        public string BucketName => $"{ProjectId}-files";
        public string ProjectId => Environment.GetEnvironmentVariable("GCLOUD_PROJECT") ?? "calcium-blend-269009";

        public StorageController(ILogger<StorageController> logger, IStorageWrapper storage)
        {
            _logger = logger;
            _storage = storage;
            var exists = _storage.BucketExists(BucketName);
            if(!exists)
            {
                _storage.CreateBucket(ProjectId, BucketName);
            }
        }

        public IActionResult Index()
        {
            var result = _storage.ListObjects(BucketName);

            var list = result.Select(i => new StorageModel() { Filename = i.Name, ContentType = i.ContentType }).ToList();

            return View(list);
        }

        public async Task<IActionResult> DownloadFileAsync(string filename, string contentType)
        { 
            Response.Headers["Content-Disposition"] = $"attachment; filename={filename}";
            using(var memoryStream = new MemoryStream())
            {
                await _storage.DownloadObjectAsync(BucketName, filename, memoryStream);
                return File(memoryStream.ToArray(),contentType);
            }
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        { 
            if(file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    _storage.UploadObject(BucketName, file.FileName, file.ContentType, memoryStream, null);
                }
            }
            return RedirectToAction("Index");
        }
    }
}