using System.Net;
using System.Text.Json;
using AutoEcommerce.Errors;

namespace AutoEcommerce.Middleware;

public class ExceptionMiddleware(IHostEnvironment env,RequestDelegate next)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex,env);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment hostEnvironment)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var response=hostEnvironment.IsDevelopment()?
            new ApiErrorResponse(context.Response.StatusCode, exception.Message, exception.StackTrace):
            new ApiErrorResponse(context.Response.StatusCode,exception.Message, "Internal Server Error");
        var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        var json = JsonSerializer.Serialize(response,options);
        return context.Response.WriteAsync(json);
    }
}