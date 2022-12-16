using Microsoft.AspNetCore.Mvc;

namespace RecruitmentModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public AuthenticationController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost]
        public async Task<ActionResult<GenereicResponse<string>>> Login([FromBody] LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await uow.Users.Login(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return new GenereicResponse<string>
                {
                    Data = null,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }

        }
    }
}