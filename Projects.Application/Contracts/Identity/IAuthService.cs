using Projects.Application.Model.Identity;
using Projects.Application.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<BaseResponse<RegistrationResponse>> Register(RegistrationRequest request);
    }
}
