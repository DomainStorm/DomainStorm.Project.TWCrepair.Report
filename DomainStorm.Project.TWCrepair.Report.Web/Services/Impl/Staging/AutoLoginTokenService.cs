using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.ViewModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
    public class AutoLoginTokenService : IGetService<AutoLoginToken, string>
    {
        private readonly TokenProvider _tokenProvider;
        private readonly IInvokeMethod _invokeMethod;

        public AutoLoginTokenService(TokenProvider tokenProvider, IInvokeMethod invokeMethod)
        {
            _tokenProvider = tokenProvider;
            _invokeMethod = invokeMethod;
        }

        public async Task<AutoLoginToken> GetAsync(string id)
        {
            var autologinToken = await _invokeMethod.InvokeMethodAsync<AutoLoginToken>(
              HttpMethod.Get,
              "JwtAuthApi",
              "api/user/autoLoginToken",
              _tokenProvider
              );
            return autologinToken;
        }

        public Task<AutoLoginToken> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }

        public Task<AutoLoginToken[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AutoLoginToken[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AutoLoginToken[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
