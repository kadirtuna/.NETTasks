using Services.Shared.DTO;

namespace WebAppBlazor.ClientServices
{
    public interface IAuthService
    {
        public Task<bool> RegistToken(AuthenticationDTO authenticationDTO);
    }
}
