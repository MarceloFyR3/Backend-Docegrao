using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DoceGrao.Api.Domain.Models.Settings;
using DoceGrao.Api.Domain.Models.User;
using DoceGrao.Api.Domain.Models.ValueObjects;
using DoceGrao.Api.Domain.Repositories.User;
using DoceGrao.Api.Domain.Services.User;
using DoceGrao.Api.Domain.ViewModels.User;
using DoceGrao.Api.Shared.Utilits;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DoceGrao.Api.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenManagement _tokenManagement;

        public UserService(IUserRepository userRepository, IOptions<TokenManagement> tokenManagement)
        {
            _userRepository = userRepository;
            _tokenManagement = tokenManagement.Value;
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

        public ReturnData<AuthenticateResponse> Authenticate(Credential credential)
        {
            var returnUser = _userRepository.Authenticate(credential);

            if (!returnUser.Sucesso) return new ReturnData<AuthenticateResponse>(returnUser.Sucesso, returnUser.Message, null);

            var token = generateJwtToken(returnUser.Data);

            return new ReturnData<AuthenticateResponse>(returnUser.Sucesso, returnUser.Message, new AuthenticateResponse(returnUser.Data, token));
        }

        private string generateJwtToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenManagement.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenManagement.Issuer,
                Audience = _tokenManagement.Audience,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "NomeGrupo"),
                    new Claim(ClaimTypes.PrimarySid, user.IdGrupo.ToString()),
                    new Claim("Cultura", "pt-BR")
                }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenManagement.AccessExpiration),                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
