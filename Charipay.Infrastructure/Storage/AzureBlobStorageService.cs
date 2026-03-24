using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Charipay.Application.Interfaces.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Storage
{
    public class AzureBlobStorageService : IFileStorageService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public AzureBlobStorageService(string connString, string containerName)
        {

            _blobContainerClient = new BlobContainerClient(connString, containerName);
            _blobContainerClient.CreateIfNotExists(PublicAccessType.Blob);
        }

        public async Task DeleteAsync(string fileUrl, CancellationToken token)
        {
            var uri = new Uri(fileUrl);
            var blobName = uri.AbsolutePath.TrimStart('/').Split('/', 2)[1];

            var blobClient = _blobContainerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync(cancellationToken: token);

        }

        public async Task<string> UploadAsync(Stream stream, string contentType, string fileName, string folder, CancellationToken token)
        {
            var extension = Path.GetExtension(fileName);
            var blobName = $"{folder}/{Guid.NewGuid()}{extension}";

            var blobClient = _blobContainerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                }
            }, token);

            return blobClient.Uri.ToString();
        }
    }
}
