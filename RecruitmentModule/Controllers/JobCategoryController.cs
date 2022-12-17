

namespace RecruitmentModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobCategoryController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public JobCategoryController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

       
        [HttpGet]
        public async Task<ActionResult<GenereicResponse<List<JobCategory>>>> Get()
        {
            return new GenereicResponse<List<JobCategory>>
            {
                Data = uow.JobCategory.DbSet.ToList(),
                IsSuccess = true,
                Message = "Success",
                StatusCode = 200,
            };
        }
        
        [HttpPost]
        public async Task<ActionResult<GenereicResponse<JobCategory>>> Post([FromBody] JobCategory model) 
        {
            var res = uow.JobCategory.Add(model);
            uow.DbContext.SaveChanges();
            return new GenereicResponse<JobCategory>
            {
                Data = res,
                IsSuccess = true,
                Message = "Success",
                StatusCode = 200,
            };
        }
      
    }
}