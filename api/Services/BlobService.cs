using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using fileuplaoder.common.Models;

namespace fileuploader.api.services
{
    public interface IBlobService
    {

        Task<BlobModel> UploadBlob(Stream content, string contentType, Dictionary<string, string> tags);

    }


    public class BlobService : IBlobService
    {

        private readonly BlobServiceClient _blobServiceClient;

        private const string TempContainer = "tmp";


        public BlobService(BlobServiceClient blobServiceClient)
        {
            this._blobServiceClient = blobServiceClient;
        }

        public async Task<BlobModel> UploadBlob(Stream content, string contentType, Dictionary<string, string> tags)
        {
            try
            {
                BlobContainerClient _containerClient = _blobServiceClient.GetBlobContainerClient(TempContainer);
                var blobName = GenerateFileName(contentType);

                _containerClient.CreateIfNotExists();
                IProgress<long> ph = new Progress<long>(progress =>
                {
                    Console.WriteLine(progress.ToString());
                });
                content.Position = 0;
                var _blobclient = _containerClient.GetBlobClient(blobName);
                var T = await _blobclient.UploadAsync(content, null, tags, null, progressHandler: ph);                
                var c = _blobclient.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Read, DateTimeOffset.Now.AddMinutes(1)).AbsoluteUri.ToString();
                return new BlobModel
                {
                    Src = new SrcModel(c),
                    Tags = _blobclient.GetTags().Value.Tags.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
                };
            }
            catch
            {
                return null;
            }


        }

        private string GenerateFileName(string contentType)
        {
            try
            {
                return Guid.NewGuid().ToString() + "." + contentType.Split('/').Last();
            }
            catch
            {
                throw;
            }
        }
    }
}