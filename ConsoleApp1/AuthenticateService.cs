using System;
using System.Collections.Generic;
using System.Text;
using WX.MicroService.Model;

namespace WX.MicroService.Interface
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(LoginRequestDTO request, out string token);
    }
}
