

namespace RecruitmentModule.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<GenereicResponse<List<Vacancy>>>> Get(int? id)
        {
            if(id == null)
            {
                return new GenereicResponse<List<Vacancy>>
                {
                    Data = uow.Vacancy.DbSet.Include(x => x.Applicants).Include(x => x.JobCategory).Include(x => x.Responsibilities).Include(x => x.Skills).Where(x => x.IsDelete == false).ToList(),
                    IsSuccess = true,
                    Message = "Success",
                    StatusCode = 200,
                };
            }
            else
            {
                return new GenereicResponse<List<Vacancy>>
                {
                    Data = uow.Vacancy.DbSet.Include(x => x.Applicants).Include(x => x.JobCategory).Include(x => x.Responsibilities).Include(x => x.Skills).Where(x => x.IsDelete == false).Where(x=>x.ID == id).ToList(),
                    IsSuccess = true,
                    Message = "Success",
                    StatusCode = 200,
                };
            }
           
        }

        [HttpPost("ApplyApplicant")]
        public async Task<GenereicResponse<string>> ApplyApplicant(ApplayApplicantViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var _Applicant = uow.Users.GetAsNoTracking().Where(x => x.Id == model.ApplicantId).FirstOrDefault();
                    var vacancy = uow.Vacancy[model.vacancyId];
                    
                    if (vacancy != null && _Applicant != null)
                    {
                        Applicant applicant = uow.Mapper.Map<Applicant>(_Applicant);
                        vacancy.Applicants.Add(applicant);
                    }
                }
                else
                {
                    return new GenereicResponse<string> { IsSuccess = false, StatusCode = 400, Message = "InValid Data", Data = null };

                }
                return new GenereicResponse<string> { IsSuccess = true, StatusCode = 200, Message = "success", Data = null };
            }

            catch (Exception ex)
            {
                return new GenereicResponse<string> { IsSuccess = false, StatusCode = 500, Message = ex.Message, Data = null };
            }
        }


        [HttpGet("GetVacanciesPaging")]
        public async Task<ActionResult<GenereicResponse<List<Vacancy>>>> GetVacanciesPaging(int PageNumber,int PageSize) 
        {
            return new GenereicResponse<List<Vacancy>>
            {
                Data = uow.Vacancy.Paging(PageNumber, PageSize).Include(x => x.Applicants).Include(x=>x.JobCategory).Include(x=>x.Responsibilities).Include(x=>x.Skills).Where(x => x.IsDelete == false).ToList(),
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


        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult<GenereicResponse<object>>> Delete(int id) 
        {
            var vacancy = uow.Vacancy.Find(id);
            if (vacancy == null)
            {
                return new GenereicResponse<object>
                {
                    Data = 1,
                    IsSuccess = false,
                    Message = "Job Vacancy Not Found",
                    StatusCode = 400
                };
            }
            vacancy.IsDelete = true;
            uow.DbContext.SaveChanges();
            return new GenereicResponse<object>
            {
                Data = 1,
                IsSuccess = true,
                Message = "Success",
                StatusCode = 200
            };
        }
    }
}