

using AutoMapper;
using RM.Shared;
using RM.Shared.ViewModels;

namespace RecruitmentModule.AutoMapper
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<AppUser, UserRegisterViewModel>().ReverseMap(); 
            CreateMap<Vacancy, VacancyViewModel>().ReverseMap(); 
            CreateMap<Responsibilities, ResponsibilitiesViewModel>().ReverseMap(); 
            CreateMap<Skills, SkillsViewModel>().ReverseMap(); 
        }
    }
}
