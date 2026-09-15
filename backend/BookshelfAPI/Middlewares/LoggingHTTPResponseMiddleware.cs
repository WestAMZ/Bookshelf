namespace AuthorsWebAPI.Middlewares
{
    public static class LoggingHTTPResponseMiddlewareExtensions 
    {
        public static IApplicationBuilder UseLoggingHTTPResponse(this IApplicationBuilder app) 
        {

            return app.UseMiddleware<LoggingHTTPResponseMiddleware>();
        }
    }
    public class LoggingHTTPResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LoggingHTTPResponseMiddleware> logger;

        public LoggingHTTPResponseMiddleware(RequestDelegate next, ILogger<LoggingHTTPResponseMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        //this method is necesary to work and interact with other middlewares
        public async Task InvokeAsync(HttpContext context) 
        {
            using (var ms = new MemoryStream())
            {
                // this section is executed in the request
                var originalBodyRequest = context.Response.Body;
                context.Response.Body = ms;

                await next.Invoke(context);

                // this section is executed when nexts middleware send a response
                ms.Seek(0, SeekOrigin.Begin);
                string response = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(originalBodyRequest);
                context.Response.Body = originalBodyRequest;
                logger.LogInformation(response);
            }
        }
    }
}
