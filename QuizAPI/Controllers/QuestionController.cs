using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;
using QuizAPI.DTOs.Questions;
using QuizAPI.DTOs.Quzzes;
using QuizAPI.Entites;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public QuestionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] QuestionPutDto dto) // PUT
        {

            var question = _context.Questions.FirstOrDefault(x => x.Id == id);
            if (question is null) return NotFound();

            _mapper.Map(dto, question);

            _context.SaveChanges();

            return Ok(question.Id);
        }


    }
}
