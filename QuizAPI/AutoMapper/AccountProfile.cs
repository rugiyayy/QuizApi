using AutoMapper;
using QuizAPI.DTOs.Account;
using QuizAPI.DTOs.Questions;
using QuizAPI.Entites;

namespace QuizAPI.AutoMapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<SignUpDto, AppUser>();
            CreateMap<SignInDto, AppUser>();//in both vases : from dto to entity

        }
    }
}
