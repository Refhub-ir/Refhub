using Microsoft.Extensions.Options;
using Refhub.Models.AppSetting;
using Refhub.Service.Interface;

namespace Refhub.Service.Implement
{

    using Amazon;
    using Amazon.Runtime;
    using Amazon.S3;
    using Amazon.S3.Model;
    using Refhub.Models.Enums;
    using Refhub.Tools.Exceptions;
    using System.Threading;

    public class S3FileUploaderService : IFileUploaderService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IOptions<S3Configuration> _s3Options;

        public S3FileUploaderService(IOptions<S3Configuration> s3Options)
        {
            this._s3Options = s3Options;

            var credentials = new BasicAWSCredentials(s3Options.Value.AccessKey, s3Options.Value.SecretKey);

            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(s3Options.Value.Region), // Dynamically set region endpoint
                ServiceURL = s3Options.Value.ServiceURL,
                ForcePathStyle = true // بسیار مهم برای کار با آروان‌کلاد
            };

            _s3Client = new AmazonS3Client(credentials, config);

            //_bucketName = s3Options.Value.BucketName;
        }
        private string GenerateS3Url(string key, string bucketName)
        {
            // برای آروان کلاد:
            return $"{_s3Options.Value.ServiceURL}/{bucketName.ToLower()}/{key}";
        }


        private string GetKey(string realUrl, string bucketName)
        {
            // برای آروان کلاد:

            var prefix = $"{_s3Options.Value.ServiceURL}/{bucketName.ToLower()}/";

            if (realUrl.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return realUrl.Substring(prefix.Length);
            }

            throw new ArgumentException("آدرس مورد نظر با پیشوند تعیین‌شده شروع نمی‌شود.");


        }

        public async Task<string> UploadFile(IFormFile file, string directoryName, string name)
        {
            var bucketName = directoryName;
            var key = $"{name.Replace(" ", "-")}{Path.GetExtension(file.FileName)}";

            using var stream = file.OpenReadStream();
            var request = new PutObjectRequest
            {
                BucketName = bucketName.ToLower(),
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType,

                CannedACL = S3CannedACL.PublicRead
            };

            await _s3Client.PutObjectAsync(request);
            return GenerateS3Url(key, bucketName);
        }

        public async Task DeleteFile(string realUrl, string? bucketName)
        {

            var key = GetKey(realUrl, bucketName);

            var request = new DeleteObjectRequest
            {
                BucketName = bucketName.ToLower(),
                Key = key
            };

            await _s3Client.DeleteObjectAsync(request);
        }

        public async Task<Stream> DownloadFileAsync(string fileName, CancellationToken ct, string? bucketName)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName.ToLower(),
                Key = fileName // مثلاً: "images/profile.jpg"
            };

            try
            {
                using var response = await _s3Client.GetObjectAsync(request, ct);
                var memoryStream = new MemoryStream();
                await response.ResponseStream.CopyToAsync(memoryStream, ct);
                memoryStream.Position = 0; // مهم! برای اینکه موقع Return از ابتدا خونده بشه
                return memoryStream;
            }
            catch (AmazonS3Exception ex)
            {
                // مثلاً اگر فایل وجود نداشت یا کلید اشتباه بود
                throw new FileDownloadException("خطا در دانلود فایل از S3", ex);
            }
        }


    }


}
