using System;
using System.Collections.Generic;
using WX.MicroService.Model;

namespace WX.MicroService.Interface
{
    public interface IUserService
    {
        User FindUser(int id);

        IEnumerable<User> UserAll();
    }
}
