
using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace fileuplaoder.swa.helpers
{



public class AzureStorageAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public AzureStorageAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigationManager)
        : base(provider, navigationManager)
    {
        ConfigureHandler(
            authorizedUrls: new[] { "https://sceblobs.blob.core.windows.net/" },
            scopes: new[] { "https://storage.azure.com/user_impersonation" });
    }
}
}