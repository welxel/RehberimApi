using AppCore.Business.Models.Results;
using AppCore.Business.Services.Bases;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Bases {
    public interface IUserService:IService<UsersModel> {
        Result IgnoreUser(int id);
        Result GetUserById(int id);
    }
}
