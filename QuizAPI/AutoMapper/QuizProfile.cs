using AutoMapper;
using QuizAPI.DTOs.Quzzes;
using QuizAPI.Entites;

namespace QuizAPI.AutoMapper
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizGetDto>();
            CreateMap<Quiz, QuizDetailedGetDto>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));


            CreateMap<QuizPutDto, Quiz>();
            CreateMap<QuizPostDto, Quiz>();
        
        }

    }
}
