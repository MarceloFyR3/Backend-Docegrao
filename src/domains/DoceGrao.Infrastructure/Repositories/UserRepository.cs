using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoceGrao.Api.Domain.Models.User;
using DoceGrao.Api.Domain.Models.ValueObjects;
using DoceGrao.Api.Domain.Repositories.User;
using DoceGrao.Api.Domain.ViewModels.User;
using DoceGrao.Api.Shared.Utilits;
using DoceGrao.Database.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using NetHacksPack.Data.Persistence.Abstractions;
using NetHacksPack.Data.Persistence.EF;

namespace DoceGrao.Api.Infrastructure.Repositories
{
    public class UserRepository : EntityFrameworkRepository<UserModel>, IUserRepository
    {
        #region Property 

        private readonly IUnityOfWork<ApplicationContext> _unityOfWork;

        #endregion

        #region Startup
        public UserRepository(IUnityOfWork<ApplicationContext> unityOfWork) : base(unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        #endregion

        #region Methods

        public async Task<ReturnData<UserModel>> Register(UserModel model)
        {
            try
            {
                await _unityOfWork.GetRepositoryContext<ApplicationContext>().Set<UserModel>().AddAsync(model);
                await _unityOfWork.GetRepositoryContext<ApplicationContext>().SaveChangesAsync();

                return new ReturnData<UserModel>(true, new List<string> { "Usuário cadastro com sucesso!" }, model);
            }
            catch (Exception e)
            {
                return new ReturnData<UserModel>(false, new List<string> { e.Message }, model);
            }
        }

        public async Task<ReturnData<UserModel>> GetById(string idUsuario)
        {
            try
            {
                var user = await _unityOfWork.GetRepositoryContext<ApplicationContext>().Set<UserModel>().FirstAsync(u => u.Id == Guid.Parse(idUsuario));

                return user != null ? new ReturnData<UserModel>(true, new List<string> { "Usuário cadastro com sucesso!" }, user) : new ReturnData<UserModel>(false, new List<string> { "Usuário não encontrado!" }, null);
            }
            catch (Exception e)
            {
                return new ReturnData<UserModel>(false, new List<string> { e.Message }, null);
            }
        }

        public async Task<ReturnData<object>> Update(UserUpdateViewModel model)
        {
            try
            {
                var user = await _unityOfWork.GetRepositoryContext<ApplicationContext>().Set<UserModel>().FirstAsync(u => u.Id == Guid.Parse(model.Id));

                if (user == null)
                    return new ReturnData<object>(false, new List<string> { "Usuário não encontrado!" }, null);
                user.Name = model.Name;
                if (model.IdGrupo != null) user.IdGrupo = (int)model.IdGrupo;

                _unityOfWork.GetRepositoryContext<ApplicationContext>().Set<UserModel>().Update(user);
                await _unityOfWork.GetRepositoryContext<ApplicationContext>().SaveChangesAsync();

                return new ReturnData<object>(true, new List<string> { "Usuário atualizado com sucesso" }, null);

            }
            catch (Exception e)
            {
                return new ReturnData<object>(false, new List<string> { e.Message }, null);
            }
        }

        public ReturnData<UserModel> Authenticate(Credential credential)
        {
            try
            {
                var message = "Usuario e Senha inválido";
                var success = false;

                var user = _unityOfWork.GetRepositoryContext<ApplicationContext>().Set<UserModel>().FirstOrDefault(u => u.Credential.Login.Equals(credential.Login));
                if (user == null)
                    return new ReturnData<UserModel>(false, new List<string> { message }, null);

                // check if password is correct
                if (!Hash.CompareHashValue(credential.Password, user.Email.Address, user.Credential.Password, user.SaltKey))
                {
                    user.IncorrectAttempts = Convert.ToInt32(user.IncorrectAttempts.GetValueOrDefault() + 1);

                    if (user.IncorrectAttempts >= 5)
                    {
                        user.Block = true;
                        user.DateBlock = DateTime.Now;

                        message = "O número máximo de tentativas de login foi atingido, por favor, recupere sua senha.";
                    }
                }
                else
                {
                    user.IncorrectAttempts = 0;

                    success = true;
                    message = "Usuário autorizado!";
                }

                _unityOfWork.GetRepositoryContext<ApplicationContext>().Set<UserModel>().Update(user);
                _unityOfWork.GetRepositoryContext<ApplicationContext>().SaveChanges();

                return new ReturnData<UserModel>(success, new List<string> { message }, success == false ? null : user);
            }
            catch (Exception e)
            {
                return new ReturnData<UserModel>(false, new List<string> { e.Message }, null);
            }
        }

        #endregion


    }
}
