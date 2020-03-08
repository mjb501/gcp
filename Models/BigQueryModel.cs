using System;
using System.Collections.Generic;
using Google.Cloud.Datastore.V1;
using Microsoft.AspNetCore.Http;

namespace gcp.Models
{
    public class BigQueryModel
    {
        public int DatastoreRequests { get; set; }
        public int StorageRequests { get; set; }
    }
}