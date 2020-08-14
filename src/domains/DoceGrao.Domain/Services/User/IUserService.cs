using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DoceGrao.Api.Domain.Models.User;
using DoceGrao.Api.Domain.Models.ValueObjects;
using DoceGrao.Api.Domain.ViewModels.User;
using DoceGrao.Api.Shared.Utilits;

namespace DoceGrao.Api.Domain.Services.User
{
    public interface IUserService
    {
        Task<ReturnData<UserCreateViewModel>> Register(UserCreateViewModel form);
        Task<ReturnData<UserModel>> GetById(string idUsuario);
        Task<ReturnData<object>> Update(UserUpdateViewModel form);
        ReturnData<AuthenticateResponse> Authenticate(Credential credential);
    }
}
