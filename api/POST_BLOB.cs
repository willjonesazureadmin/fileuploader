
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using fileuploader.api.services;
using Azure.Storage.Blobs;
using System.Linq;
using System.Collections.Generic;
using fileuploader.common.models;

namespace fileuploader.api
{
    public class POST_BLOB
    {

        private readonly BlobService _blobService;

        public POST_BLOB(BlobService blobService)
        {
            this._blobService = blobService;
        }

        [FunctionName("post_blob")]
        public async Task<IActionResult> post_blob([HttpTrigger(AuthorizationLevel.Function, "post", Route = "blob/$value")] HttpRequest req, ILogger log, [FromHeader] Dictionary<string, string> tags)
        {
            string contentType = req.ContentType;
            Stream uploadStream = new MemoryStream();
            await req.Body.CopyToAsync(uploadStream);
            if (contentType == "image/jpeg" || contentType == "image/png")
            {
                try
                {
                    var c = await _blobService.UploadBlob(uploadStream, contentType, tags);
                    return new OkObjectResult(new ResponseModel
                    {
                        status = status.success
                    }
                    );

                }
                catch(Exception e)
                {
                    return new BadRequestObjectResult(new ResponseModel
                    {
                        status = status.failure,
                        Message = e.Message,
                    });
                }
            }
            else
            {
                return new BadRequestObjectResult(new ResponseModel
                {
                    status = status.failure,
                    Message = "Invalid Image Format"
                });
            }
        }
    }
}