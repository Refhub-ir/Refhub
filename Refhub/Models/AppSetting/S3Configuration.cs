namespace Refhub.Models.AppSetting
{
    public class S3Configuration
    {
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string ServiceURL { get; set; }
    }
}
