using AutoMapper;
using QuizAPI.DTOs.Questions;
using QuizAPI.DTOs.Quzzes;
using QuizAPI.Entites;

namespace QuizAPI.AutoMapper
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionGetDto>()
           .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options));

            CreateMap<QuestionPutDto, Question>();
            CreateMap<QuestionPostDto, Question>();
        }
    }
}
