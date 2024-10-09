namespace Client.Middlewares
{
    public class JWTsMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Session.GetString("JWT");
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", $"Bearer {token}");
            }

            await _next(context);
        }
    }
}
