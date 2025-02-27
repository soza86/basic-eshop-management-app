using System.Net.Http.Headers;

namespace CSharpApp.Infrastructure.ExternalServices
{
    public class JwtAuthHandler : DelegatingHandler
    {
        private readonly IJwtTokenService _jwtTokenService;

        public JwtAuthHandler(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _jwtTokenService.GetToken(cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
