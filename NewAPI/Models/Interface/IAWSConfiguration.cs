using MySqlX.XDevAPI.Relational;

namespace NewAPI.Models.Interface
{
    public interface IAWSConfiguration
    {
        string AwsAccessKey { get; set; }
        string AwsSecretAccessKey { get; set; }
        string AwsSessionToken { get; set; }
        string BucketName { get; set; }
        string Region { get; set; }
    }
}
