using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;

namespace NewAPI.Services
{
    public class AwsS3Service
    {
        private readonly string _bucketName;
        private readonly IAmazonS3 _awsS3Client;

        public AwsS3Service(string awsAccessKeyId, string awsSecretAccessKey, string region, string bucketName)
        {
            _bucketName = bucketName;
            _awsS3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.GetBySystemName(region));
        }


        public async Task<byte[]> DownloadFileAsync(string file)
        {
            MemoryStream _memoryStream = null;

            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = file
                };

                using (var response = await _awsS3Client.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (_memoryStream = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(_memoryStream);
                        }
                    }
                }

                if (_memoryStream is null || _memoryStream.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", file));

                return _memoryStream.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetFileUrl(string fileName, int expiresInMinutes = 60)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                Expires = DateTime.UtcNow.AddYears(2)
            };

            string url = _awsS3Client.GetPreSignedURL(request);

            return url;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string urlNewObject;
            try
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    Guid guid = Guid.NewGuid();
                    string randomId = guid.ToString();

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = randomId,
                        BucketName = _bucketName,
                        ContentType = file.ContentType
                    };

                    var fileTransferUtility = new TransferUtility(_awsS3Client);

                    await fileTransferUtility.UploadAsync(uploadRequest);

                    urlNewObject = "https://" + _bucketName + ".s3.amazonaws.com/" + randomId;

                    return urlNewObject;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteFile(string fileName, string versionId = null)
        {
            DeleteObjectRequest request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            if (!string.IsNullOrEmpty(versionId))
                request.VersionId = versionId;

            await _awsS3Client.DeleteObjectAsync(request);
        }

        public bool IsFileExists(string fileName, string versionId = null)
        {
            try
            {
                GetObjectMetadataRequest request = new GetObjectMetadataRequest()
                {
                    BucketName = _bucketName,
                    Key = fileName,
                    VersionId = !string.IsNullOrEmpty(versionId) ? versionId : null
                };

                var response = _awsS3Client.GetObjectMetadataAsync(request).Result;

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is AmazonS3Exception awsEx)
                {
                    if (string.Equals(awsEx.ErrorCode, "NoSuchBucket"))
                        return false;

                    else if (string.Equals(awsEx.ErrorCode, "NotFound"))
                        return false;
                }

                throw;
            }
        }

        public IFormFile ConvertBase64ToFormFile(string base64Value)
        {
            Guid newId = Guid.NewGuid();

            byte[] bytes = Convert.FromBase64String(base64Value);

            var stream = new MemoryStream(bytes);
            IFormFile file = new FormFile(stream, 0, bytes.Length, "file", newId.ToString())
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream" // Define o tipo de conteúdo apropriado aqui
            };
            // Cria um MemoryStream a partir dos bytes

            return file;
        }
    }
}
