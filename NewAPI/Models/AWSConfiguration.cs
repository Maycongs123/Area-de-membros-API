using Amazon.S3;
using NewAPI.Models.Interface;

namespace NewAPI.Models
{
    public class AWSConfiguration : IAWSConfiguration
    {
        public AWSConfiguration()
        {
            BucketName = "filesareamembers";
            Region = "us-east-1";
            AwsAccessKey = "AKIAXFPFSHERIREP5MKZ";
            AwsSecretAccessKey = "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";
            AwsSessionToken = "";
        }

        public string BucketName { get; set; }
        public string Region { get; set; }
        public string AwsAccessKey { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public string AwsSessionToken { get; set; }
    }  
}
