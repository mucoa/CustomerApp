using Microsoft.AspNetCore.Http.Extensions;
using System.Text;

namespace CustomerApp.WebAPI.Helpers;

public static class HttpRequestHelper
{
    public static async Task<object?> GetRawLog(this HttpRequest request)
    {
        object? log;

        if (request is null)
        {
            return new
            {
                request = "Could not resolved"
            };
        }

        using var streamReader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);

        var data = await streamReader.ReadToEndAsync().ConfigureAwait(false);

        log = new
        {
            method = request.Method + " " + request.GetDisplayUrl(),
            protocol = request.Protocol,
            headers = request.Headers,
            body = data,
        };

        return log;
    }
}
