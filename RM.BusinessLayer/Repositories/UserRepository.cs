using RM.Shared.ViewModels;
using RM.Shared.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using RM.BusinessLayer.Helpers;

namespace RM.BusinessLayer.IRepositories
{
    #region Definitions
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        Task<GenereicResponse<string>> RegisterEmployee(UserRegisterViewModel model);
        Task<GenereicResponse<string>> Login(LoginViewModel model);
        Task<GenereicResponse<string>> AddRoleAsync(AssignRoleToUserViewModel model);

    }
    #endregion

    #region Implementation Section
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        private readonly IUnitOfWork uow;
        private readonly JWT _jwt;


        public AppUserRepository(DatabaseContext db, IUnitOfWork uow, IOptions<JWT> jwt) : base(db, uow)
        {
            this.uow = uow;
            _jwt = jwt.Value;

        }

        public async Task<GenereicResponse<string>> RegisterEmployee(UserRegisterViewModel model)
        {
            try
            {
                var RoleExist = uow.Roles.Find(Role.Applicant);
                if (RoleExist != null)
                    return new GenereicResponse<string>
                    {
                        Data = null,
                        Message = $"Role {Role.Applicant}  Not Exist",
                        StatusCode = 400
                    };

                var user = uow.Mapper.Map<AppUser>(model);
                var result = await uow.UserManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Empty;

                    foreach (var error in result.Errors)
                        errors += $"{error.Description},";

                    return new GenereicResponse<string> { Message = errors, IsSuccess = false, StatusCode = 400 };
                }

                #region Assign Default Role To User
                AppUser ReturenUser = await uow.UserManager.FindByIdAsync(user.Id);
                await uow.UserManager.AddToRoleAsync(ReturenUser, Role.Applicant);
                #endregion

                #region Create Toaken
                var jwtSecurityToken = await CreateJwtToken(user);
                #endregion



                #region return Authentication Model 
                return new GenereicResponse<string>
                {
                    Data = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Message = "Success",
                    StatusCode = 200
                };
                #endregion
            }
            catch (Exception ex)
            {
                return new GenereicResponse<string>
                {
                    Message = ex.Message,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }

        }

        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await uow.UserManager.GetClaimsAsync(user);
            var roles = await uow.UserManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("Role", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<GenereicResponse<string>> Login(LoginViewModel model)
        {
            #region Check If User Exist 
            var user = await uow.UserManager.FindByNameAsync(model.UserName);
            #endregion
            #region Check If UserName & Passowrd  =>  Correct
            if (user is null || !await uow.UserManager.CheckPasswordAsync(user, model.Password))
            {
                return new GenereicResponse<string>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Username Or Password Invalid",
                    StatusCode = 400
                };
            }
            #endregion

            var jwtSecurityToken = await CreateJwtToken(user);

            return new GenereicResponse<string>
            {
                Data = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Message = "Success",
                StatusCode = 200
            };
        }

        public async Task<GenereicResponse<string>> AddRoleAsync(AssignRoleToUserViewModel model)
        {
            var user = await uow.UserManager.FindByIdAsync(model.UserId);

            if (user is null || !await uow.RoleManager.RoleExistsAsync(model.Role.ToString()))
                return new GenereicResponse<string>
                {
                    Data = null,
                    Message = "Invalid user ID or Role",
                    StatusCode = 400
                };

            if (await uow.UserManager.IsInRoleAsync(user, model.Role.ToString()))
                return new GenereicResponse<string>
                {
                    Data = null,
                    Message = "User already assigned to this role",
                    StatusCode = 400
                };

            var result = await uow.UserManager.AddToRoleAsync(user, model.Role.ToString());
            return new GenereicResponse<string>
            {
                Data = null,
                Message = result.Succeeded ? string.Empty : "Sonething went wrong",
                StatusCode = 400
            };
        }
    }
    #endregion
}
