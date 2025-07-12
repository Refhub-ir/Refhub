using Microsoft.Extensions.Options;
using Refhub.Models.AppSetting;
using Refhub.Service.Interface;

namespace Refhub.Service.Implement
{
   
        using Amazon;
        using Amazon.Runtime;
        using Amazon.S3;
        using Amazon.S3.Model;
        using System.Threading;

        public class S3FileUploaderService : IFileUploaderService
        {
            private readonly string _bucketName;
            private readonly string _region;
            private readonly IAmazonS3 _s3Client;
            private readonly IOptions<S3Configuration> _s3Options;

            public S3FileUploaderService(IOptions<S3Configuration> s3Options)
            {
                this._s3Options = s3Options;

                var credentials = new BasicAWSCredentials(s3Options.Value.AccessKey, s3Options.Value.SecretKey);

                var config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.USEast1, // منطقه ساختگی، چون ArvanRegion اختصاصی داره
                    ServiceURL = s3Options.Value.ServiceURL,
                    ForcePathStyle = true // بسیار مهم برای کار با آروان‌کلاد
                };

                _s3Client = new AmazonS3Client(credentials, config);

                _region = s3Options.Value.Region;
                _bucketName = s3Options.Value.BucketName;
            }
            private string GenerateS3Url(string key)
            {
                // برای آروان کلاد:
                return $"{_s3Options.Value.ServiceURL}/{_bucketName}/{key}";
            }


            private string GetKey(string realUrl)
            {
                // برای آروان کلاد:

                return realUrl.Replace($"{_s3Options.Value.ServiceURL}/{_bucketName}/", "");

            }
            public async Task<string> UploadFile(IFormFile file, string directoryName, string type, string name)
            {
                var bucketName = _bucketName;
                var key = $"{directoryName}/{type}/{name.Replace(" ", "-")}{Path.GetExtension(file.FileName)}";

                using var stream = file.OpenReadStream();
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = file.ContentType,

                    CannedACL = S3CannedACL.PublicRead
                };

                await _s3Client.PutObjectAsync(request);
                return GenerateS3Url(key);
            }


            public async Task DeleteFile(string realUrl)
            {

                var key = GetKey(realUrl);

                var request = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };

                await _s3Client.DeleteObjectAsync(request);
            }

            public async Task<Stream> DownloadFileAsync(string fileName, CancellationToken ct)
            {
                var request = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileName // مثلاً: "images/profile.jpg"
                };

                try
                {
                    using var response = await _s3Client.GetObjectAsync(request,ct);
                    var memoryStream = new MemoryStream();
                    await response.ResponseStream.CopyToAsync(memoryStream,ct);
                    memoryStream.Position = 0; // مهم! برای اینکه موقع Return از ابتدا خونده بشه
                    return memoryStream;
                }
                catch (AmazonS3Exception ex)
                {
                    // مثلاً اگر فایل وجود نداشت یا کلید اشتباه بود
                    throw new Exception($"خطا در دانلود فایل از S3: {ex.Message}", ex);
                }
            }

            
        }
    

}
