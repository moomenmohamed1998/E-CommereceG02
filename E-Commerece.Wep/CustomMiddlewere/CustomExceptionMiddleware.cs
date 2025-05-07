using System.Text.Json;
using Domain.Exceptions;
using Shared.ErrorModelse;

namespace E_Commerece.Wep.CustomMiddlewere
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionMiddleware> logger;

        public CustomExceptionMiddleware(RequestDelegate Next, ILogger<CustomExceptionMiddleware> logger)
        {
            next = Next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);


                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var response = new ErrorToReturn()
                    {
                        statusCode = httpContext.Response.StatusCode,
                        ErrorMassage = $"End point{httpContext.Request.Path} is not Found",
                    };
                    var ResponseToReturn = JsonSerializer.Serialize(response);
                    await httpContext.Response.WriteAsync(ResponseToReturn);
                }

            }



            catch (Exception ex)
            {
                logger.LogError(ex, "Something Wrong");

                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundExceptions => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                httpContext.Response.ContentType = "application/json";
                var response = new ErrorToReturn()
                {
                    statusCode = httpContext.Response.StatusCode,
                    ErrorMassage = ex.Message,
                };
                var ResponseToReturn = JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsync(ResponseToReturn);

            }
        }
    }
}
