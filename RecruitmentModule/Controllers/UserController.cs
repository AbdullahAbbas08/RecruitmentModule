using Microsoft.AspNetCore.Mvc;


namespace RecruitmentModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public UserController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost]
        public async Task<GenereicResponse<string>> CreateUser(UserRegisterViewModel Model)
        {
            try
            {
                return await uow.Users.RegisterEmployee(Model);
            }

            catch (Exception ex)
            {
                return new GenereicResponse<string> { IsSuccess = false, StatusCode = 500, Message = ex.Message, Data = null };
            }
        }
    }
}