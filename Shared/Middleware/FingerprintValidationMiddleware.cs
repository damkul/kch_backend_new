using kch_backend.Shared.Utils;

namespace kch_backend.Shared.Middleware
{
    public class FingerprintValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public FingerprintValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var fingerprintHeader = context.Request.Headers["X-Device-Fingerprint"].ToString();

            if (!string.IsNullOrEmpty(fingerprintHeader))
            {
                var userAgent = context.Request.Headers["User-Agent"].ToString();
                var ip = context.Connection.RemoteIpAddress?.ToString();

                if (!FingerprintHelper.ValidateFingerprint(fingerprintHeader, userAgent, ip))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Invalid device fingerprint");
                    return;
                }
            }

            await _next(context);
        }
    }

    // Extension method to use in Program.cs
    public static class FingerprintValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseFingerprintValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FingerprintValidationMiddleware>();
        }
    }
}
