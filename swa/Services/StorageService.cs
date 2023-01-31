using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using System.Text;
using fileuploader.common.helpers;
using System.Collections.Specialized;
using fileuploader.common.models;
using fileuplaoder.common.Models;

namespace fileuploader.swa.Services
{

    public interface IStorageService
    {
        Task<BlobResponseModel> UploadBlob(StreamContent content, Dictionary<string,string> tags, Guid Id);
    }

    public class StorageService : IStorageService
    {
        private readonly HttpClient _http;

        public StorageService(HttpClient httpClient)
        {
            this._http = httpClient;
        }

        public async Task<BlobResponseModel> UploadBlob(StreamContent content, Dictionary<string,string> tags, Guid id)
        {

            try
            {
                NameValueCollection nvc = new NameValueCollection();
                // QueryStringBuilder.Build()
                var u = $"api/blob/{id}/$value";
                var r = await _http.PatchAsync(u, content);

                if(r.IsSuccessStatusCode)
                {
                    try
                    {
                        BlobResponseModel b = new BlobResponseModel();
                        u = $"api/blob/{id}";
                        b = await _http.GetFromJsonAsync<BlobResponseModel>(u);
                        return b;
    
                    }
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}