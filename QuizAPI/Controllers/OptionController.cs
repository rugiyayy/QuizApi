using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data;
using QuizAPI.DTOs.Options;
using QuizAPI.DTOs.Quzzes;
using System.Data;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController: ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OptionController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id , [FromBody] OptionPutDto dto)
        {
            var option = _context.Options.FirstOrDefault(x => x.Id == id);
            if (option is null) return NotFound();

            var duplicateOptions = _context.Options
                    .Where(o => o.QuestionId == option.QuestionId && o.Name == dto.Name && o.Id != id).ToList();

            if (duplicateOptions.Any())
            {
                return BadRequest("Duplicate option value found for this question.");
            }

            _mapper.Map(dto, option);
            _context.SaveChanges();
            return Ok(option.Id);
        }
    }
}
