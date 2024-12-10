using Serilog;

namespace TodoApplikasjonAPIEntityDelTre.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred, please help!"); // Log the exception details with a message for debugging purposes

                context.Response.StatusCode = 500; // Set the HTTP response status code to 500 to indicate a server error

                await context.Response.WriteAsync("An error occurred on the server. Please try again later."); // Send a user-friendly error message as the response



            }
        }
    }
}
