using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Common;
using TaskManagement.Data;
using TaskManagement.Data.DTO;
using TaskManagement.Data.Models;
using TaskManagement.Manager.Interface;

namespace TaskManagement.Manager
{
    public class UsersManager:IUsersManager
    {
        private readonly DataProvider dataProvider;
        private readonly IMapper mapper;
        public UsersManager(DataProvider dataProvider, IMapper mapper) 
        {
            this.dataProvider = dataProvider;
            this.mapper = mapper;
        }



        public async Task<LoginModel> LoginUser(UserLoginDto userLogin)
        {
            try
            {
                var mappedUser = mapper.Map<Users>(userLogin);
                var user = await dataProvider.GetAllByCondition<Users>(Constants.Tables.Users.ToString(), mappedUser);
                if (user == null || !user.Any())
                {
                    throw new Exception("You don't have Access");
                }
                string tkn = GenerateJwtToken(user.FirstOrDefault());
                return new LoginModel { UserId = user.FirstOrDefault().Id, Tkn = tkn };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<bool> Register(UsersDto user)
        {
            try
            {
                var mappedUser = mapper.Map<Users>(user);
                var userExist = await dataProvider.GetAllByCondition<Users>(Constants.Tables.Users.ToString(), new Users { Email= user.Email });
                if (userExist.Any()) throw new Exception("user exist");
                mappedUser.Id = Guid.NewGuid().ToString();
                mappedUser.CreatedAt= DateTime.UtcNow;
                mappedUser.UpdatedAt= DateTime.UtcNow;
                int effected = await dataProvider.IncertAsync(Constants.Tables.Users.ToString(), mappedUser);
                return effected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Registration not completed");
            }
        }


        private string GenerateJwtToken(Users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "user"),
            // Add additional claims as needed
        };

            var token = new JwtSecurityToken(
                //issuer: _configuration["Jwt:Issuer"],
                //audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble("60")),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
