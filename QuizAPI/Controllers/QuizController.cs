using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;
using QuizAPI.DTOs.Options;
using QuizAPI.DTOs.Questions;
using QuizAPI.DTOs.Quzzes;
using QuizAPI.Entites;
using static QuizAPI.DTOs.Options.OptionPostDto;
using static QuizAPI.DTOs.Questions.QuestionPostDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public QuizController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet(Name ="GetAll"), Authorize(Roles ="Admin, User")]
        public IActionResult GetAll() // ++works
        {
            //properties of the quiz object are being  mapped to an object of type QuizDetailedGetDto
            //from quizzes to dto
            var quizzesDto= _context.Quizzes.Select(x=>_mapper.Map<QuizGetDto>(x))
                .AsNoTracking()
                .ToList();

            return Ok(quizzesDto);
        }




        [HttpGet("{id}")]
        [Authorize(Roles ="Admin, User")]
        public IActionResult Get(int id)  // ++works
        {
            var quiz=_context.Quizzes
                 .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefault(x=>x.Id==id);
            if (quiz is null) return NotFound();



            //from a Quiz entity to a DTO
            var dto = _mapper.Map<QuizDetailedGetDto>(quiz);

            return Ok(dto);
            
        }



        [HttpPut("{id}")]  //+++ works 
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] QuizPutDto dto) // PUT
        {
            var quiz = _context.Quizzes.FirstOrDefault(x => x.Id == id);
            if (quiz is null) return NotFound();


           // from a DTO (dto)to the existing Quiz entity(quiz)
            _mapper.Map(dto, quiz);
            _context.SaveChanges();

            return Ok(quiz); // or change it to id (later)
        }




        [HttpDelete("{id}")] //+++ works 
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var quiz = _context.Quizzes.FirstOrDefault(x => x.Id == id);
            if (quiz is null) return NotFound();

            _context.Remove(quiz);
            _context.SaveChanges();

            return Ok();
        }


        [HttpPost("PostQuiz")] // ++works
        [Authorize(Roles = "Admin")]
        public IActionResult PostQuiz([FromBody] QuizPostDto quizPostDto)
        {

            //mapping the properties of the quizPostDto to a new object of type Quiz
            // from a DTO (quizPostDto) to a Quiz entity (Quiz). 
            var quizEntity = _mapper.Map<Quiz>(quizPostDto);
            _context.Add(quizEntity);
            _context.SaveChanges();

            foreach (var questionDto in quizPostDto.Questions)
            {
                var questionEntity = _mapper.Map<Question>(questionDto);
                quizEntity.Questions.Add(questionEntity);


                foreach (var optionDto in questionDto.Options)
                {
                    var duplicateOptionNames = questionDto.Options
                           .GroupBy(o => o.Name)
                           .Where(g => g.Count() > 1)
                           .Select(g => g.Key)
                           .ToList();

                    if (duplicateOptionNames.Any())
                    {
                        return BadRequest($"Duplicate option names found in question '{questionDto.Name}': {string.Join(", ", duplicateOptionNames)}");
                    }

                    var optionEntity = _mapper.Map<Option>(optionDto);
                    questionEntity.Options.Add(optionEntity);
                }
            }
            return Ok("Quiz posted successfully");
        }


    }
}
