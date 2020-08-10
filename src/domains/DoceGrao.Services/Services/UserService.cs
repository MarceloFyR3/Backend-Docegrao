using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DoceGrao.Api.Domain.Models.User;
using DoceGrao.Api.Domain.Models.ValueObjects;
using DoceGrao.Api.Domain.Repositories.User;
using DoceGrao.Api.Domain.Services.User;
using DoceGrao.Api.Domain.ViewModels.User;
using DoceGrao.Api.Shared.Utilits;

namespace DoceGrao.Api.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ReturnData<UserCreateViewModel>> Register(UserCreateViewModel form)
        {
            var salt = Hash.Get_SALT();

            var user = new UserModel(
                Guid.NewGuid(),
                0,
                form.Name,
                new Email(form.Email),
                new Credential(form.Login, Hash.Get_HASH_SHA512(form.Password, form.Email, salt)),
                salt,
                new Document(form.Document, DateTime.Now, form.TypeDocument),
                true,
                false,
                null,
                0,
                DateTime.Now,
                false,
                "default"
            );

            var result = await _userRepository.Register(user);
            form.Id = result.Data.Id;

            return new ReturnData<UserCreateViewModel>(result.Sucesso, result.Message, form);
        }

        public async Task<ReturnData<UserModel>> GetById(string idUsuario)
        {
            return await _userRepository.GetById(idUsuario);
        }

        public async Task<ReturnData<object>> Update(UserUpdateViewModel form)
        {
            return await _userRepository.Update(form);
        }

        public ReturnData<UserModel> Authenticate(Credential credential)
        {
            return _userRepository.Authenticate(credential);
        }
    }
}
