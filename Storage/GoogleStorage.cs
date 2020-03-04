using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Google;
using Google.Api.Gax;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using GoogleObject = Google.Apis.Storage.v1.Data.Object;

namespace gcp_demo.Storage
{
    public class GoogleStorage : IStorageWrapper
    {
        public GoogleStorage()
        {
            this._wrappedClient = StorageClient.Create();
		}

		public GoogleObject GetObject(string bucket, string objectName, GetObjectOptions options = null)
		{
			return _wrappedClient.GetObject(bucket, objectName, options);
		}

		public Task DownloadObjectAsync(string bucket, string objectName, Stream destination, DownloadObjectOptions options = null)
		{
			return _wrappedClient.DownloadObjectAsync(bucket, objectName, destination, options);
		}

		public GoogleObject UploadObject(string bucketName, string objectName, string contentType, MemoryStream source, UploadObjectOptions options)
		{
			return _wrappedClient.UploadObject(bucketName, objectName, contentType, source, options, null);
		}

		public Bucket GetBucket(string bucketName)
		{
			return _wrappedClient.GetBucket(bucketName, null);
		}
		public bool BucketExists(string bucketName)
		{
			Bucket bucket = null;
			try
			{ 
				bucket = _wrappedClient.GetBucket(bucketName, null);
			}
			catch(GoogleApiException ex)
			{
				return ex.HttpStatusCode != HttpStatusCode.NotFound;
			}
			return bucket != null;
		}

		public Bucket CreateBucket(string projectId, string bucketName, CreateBucketOptions options = null)
		{
			return _wrappedClient.CreateBucket(projectId, bucketName, options);
		}

		public PagedEnumerable<Objects, GoogleObject> ListObjects(string bucketName, string prefix = null)
		{
			return _wrappedClient.ListObjects(bucketName, prefix);
		}

		private readonly StorageClient _wrappedClient;
    }
}