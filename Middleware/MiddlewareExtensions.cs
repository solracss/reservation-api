namespace ReservationAPI.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlindMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlindMiddleware>();
        }
    }
}
