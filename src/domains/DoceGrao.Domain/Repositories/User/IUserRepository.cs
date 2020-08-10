using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DoceGrao.Api.Domain.Models.User;
using DoceGrao.Api.Domain.Models.ValueObjects;
using DoceGrao.Api.Domain.ViewModels.User;
using DoceGrao.Api.Shared.Utilits;

namespace DoceGrao.Api.Domain.Repositories.User
{
    public interface IUserRepository
    {
        Task<ReturnData<UserModel>> Register(UserModel model);
        Task<ReturnData<UserModel>> GetById(string idUsuario);
        Task<ReturnData<object>> Update(UserUpdateViewModel model);
        ReturnData<UserModel> Authenticate(Credential credential);

    }
}
