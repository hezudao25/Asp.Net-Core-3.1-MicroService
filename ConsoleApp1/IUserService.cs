using System;
using System.Collections.Generic;
using WX.MicroService.Model;

namespace WX.MicroService.Interface
{
    public interface IUserService
    {
        User FindUser(int id);

        IEnumerable<User> UserAll();
        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        bool IsValid(LoginRequestDTO req);
    }
}
