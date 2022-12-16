

namespace RecruitmentModule.Controllers
{
    [Authorize(Roles =Role.SuperAdmin)]
    [ApiController]
    [Route("api/[controller]")]
    public class VacancyController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public VacancyController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

       
        [HttpGet]
        public async Task<ActionResult<GenereicResponse<List<Vacancy>>>> Get()
        {
            return new GenereicResponse<List<Vacancy>>
            {
                Data = uow.Vacancy.DbSet.Include(x=>x.JobCategory).Include(x=>x.Responsibilities).Include(x=>x.Skills).ToList(),
                IsSuccess = true,
                Message = "Success",
                StatusCode = 200,
            };
        }
        
        [HttpPost]
        public async Task<ActionResult<GenereicResponse<Vacancy>>> Post([FromBody] VacancyViewModel model) 
        {
            Vacancy vacancy = uow.Mapper.Map<Vacancy>(model);
            var res = uow.Vacancy.Add(vacancy);
            uow.DbContext.SaveChanges();
            return new GenereicResponse<Vacancy>
            {
                Data = res,
                IsSuccess = true,
                Message = "Success",
                StatusCode = 200,
            };
        }
      
    }
}