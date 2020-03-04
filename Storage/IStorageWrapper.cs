using System;
using System.IO;
using System.Threading.Tasks;
using Google.Api.Gax;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using GoogleObject = Google.Apis.Storage.v1.Data.Object;

namespace gcp_demo.Storage
{
    public interface IStorageWrapper
    {
		Task DownloadObjectAsync(string bucket, string objectName, Stream destination, DownloadObjectOptions options = null);

		GoogleObject GetObject(string bucket, string objectName, GetObjectOptions options = null);

		GoogleObject UploadObject(string bucketName, string objectName, string contentType, MemoryStream source, UploadObjectOptions options);

		Bucket GetBucket(string bucketName);

		bool BucketExists(string bucketName);

		Bucket CreateBucket(string projectId, string bucketName, CreateBucketOptions options = null);

		PagedEnumerable<Objects, GoogleObject> ListObjects(string bucketName, string prefix = null);
    }
}