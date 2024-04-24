﻿namespace AIDotNet.Document.Services;

public sealed class OpenAIHttpClientHanlder(string uri) : HttpClientHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        UriBuilder uriBuilder;
        if (request.RequestUri?.LocalPath == "/v1/chat/completions")
        {
            uriBuilder = new UriBuilder(uri.TrimEnd('/') + "/v1/chat/completions");
            request.RequestUri = uriBuilder.Uri;
        }
        else if (
            request.RequestUri?.LocalPath is "/v1/embeddings" or "/embeddings")
        {
            uriBuilder = new UriBuilder(uri.TrimEnd('/') + "/v1/embeddings");
            request.RequestUri = uriBuilder.Uri;
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return response;
        }
        
        return response;
    }
}