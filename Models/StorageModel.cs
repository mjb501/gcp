using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace gcp.Models
{
    public class StorageModel
    {
        public string Filename { get; set; }

        public string ContentType { get; set; }
    }
}