using System;
using System.Collections.Generic;
using WX.MicroService.Interface;
using WX.MicroService.Model;

namespace WX.MicroService.Service
{
    public class UserService : IUserService
    {
        #region DataInit
        private List<User> _UserList = new List<User>()
        {
            new User()
            {
                Id =1,
                Account="Administrator",
                Email="577265177@qq.com",
                Name="Wang",
                Password="123456",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
            new User()
            {
                Id =2,
                Account="Xiao",
                Email="577265177@qq.com",
                Name="Xiao",
                Password="123456",
                LoginTime=DateTime.Now,
                Role="Admin"
            },
            new User()
            {
                Id =3,
                Account="Su",
                Email="577265177@qq.com",
                Name="Su",
                Password="123456",
                LoginTime=DateTime.Now,
                Role="Admin"
            }
        };
        #endregion
        public User FindUser(int id)
        {
            return this._UserList.Find(u=> u.Id == id);
        }

        public IEnumerable<User> UserAll()
        {
            return this._UserList;
        }

        //模拟测试，默认都是人为验证有效
        public bool IsValid(LoginRequestDTO req)
        {
            return true;
        }
    }
}
