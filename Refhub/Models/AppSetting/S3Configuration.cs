using System.ComponentModel.DataAnnotations;
/// <summary>
/// Strongly-typed binding for the “AWS:S3” section.  
/// Do NOT commit real keys – store them in UserSecrets / env-vars.
/// </summary>
namespace Refhub.Models.AppSetting
{
    public sealed class S3Configuration
    {
        [Required] public string Region { get; init; } = default!;
        [Required] public string AccessKey { get; init; } = default!;
        [Required] public string SecretKey { get; init; } = default!;
        [Required] public string BucketName { get; init; } = default!;
        public string? ServiceURL { get; init; }
    }
}
