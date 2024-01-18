using AutoMapper;
using QuizAPI.DTOs.Options;
using QuizAPI.Entites;

namespace QuizAPI.AutoMapper
{
    public class OptionProfile: Profile
    {
        public OptionProfile()
        {
            CreateMap<Option, OptionGetDto>();

            CreateMap<OptionGetDto, Option>();
            CreateMap<OptionPostDto, Option>();

            CreateMap<OptionPutDto, Option>();
                 //.ForMember(dest => dest.QuestionId, opt => opt.Ignore());




            //for Custom prop names 
            //.ForMember(dest => dest.CatehoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
