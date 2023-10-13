using Microsoft.AspNetCore.Mvc;
using NewAPI.Models;
using NewAPI.Models.Interface;
using NewAPI.Services;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;
using Amazon.S3.Model;

namespace NewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwsS3Controller : Controller
    {
        private readonly IAWSConfiguration _awsConfiguration;
        private readonly AwsS3Service _aws3Service;

        public AwsS3Controller(IAWSConfiguration awsConfiguration)
        {
            _awsConfiguration = awsConfiguration;
        }

        [HttpGet("{documentName}")]
        public IActionResult GetDocumentFromS3(string documentName)
        {
            try
            {
                if (string.IsNullOrEmpty(documentName))
                    return BadRequest("The 'documentName' parameter is required");


                AWSConfiguration awsConfig = new AWSConfiguration();
                string awsAccessKey = awsConfig.AwsAccessKey;
                string awsSecretAccessKey = awsConfig.AwsSecretAccessKey;
                string region = awsConfig.Region;
                string bucketName = awsConfig.BucketName;

                AwsS3Service _aws3Service = new AwsS3Service(awsAccessKey, awsSecretAccessKey, region, bucketName);
                var document = _aws3Service.DownloadFileAsync(documentName).Result;

                string sufixbase64Content = Convert.ToBase64String(document);
                string prefixbase64Content = "data:image/jpg;base64,";
                string base64Content = prefixbase64Content + sufixbase64Content;

                return Ok(base64Content);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocumentToS3(IFormFile file)
        {
            try
            {
                if (file is null || file.Length <= 0)
                    return BadRequest("File is required to upload");

                AWSConfiguration awsConfig = new AWSConfiguration();
                string awsAccessKey = awsConfig.AwsAccessKey;
                string awsSecretAccessKey = awsConfig.AwsSecretAccessKey;
                string region = awsConfig.Region;
                string bucketName = awsConfig.BucketName;

                AwsS3Service _aws3Service = new AwsS3Service(awsAccessKey, awsSecretAccessKey, region, bucketName);

                string urlNewObject = await _aws3Service.UploadFileAsync(file);

                return Content(urlNewObject);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDocumentFromS3(string documentName)
        {
            try
            {
                if (string.IsNullOrEmpty(documentName))
                    return BadRequest("The 'documentName' parameter is required");

                AWSConfiguration awsConfig = new AWSConfiguration();
                string awsAccessKey = awsConfig.AwsAccessKey;
                string awsSecretAccessKey = awsConfig.AwsSecretAccessKey;
                string region = awsConfig.Region;
                string bucketName = awsConfig.BucketName;

                AwsS3Service _aws3Service = new AwsS3Service(awsAccessKey, awsSecretAccessKey, region, bucketName);

                await _aws3Service.DeleteFile(documentName);

                return Ok(string.Format("The document '{0}' is deleted successfully", documentName));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
