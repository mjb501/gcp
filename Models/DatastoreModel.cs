using System;
using System.Collections.Generic;
using Google.Cloud.Datastore.V1;
using Microsoft.AspNetCore.Http;

namespace gcp.Models
{
    public class DatastoreModel
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public int Count {get; set;}
    }
}