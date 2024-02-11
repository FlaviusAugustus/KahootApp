using BracketMaker.Models;
using BracketMaker.Models.Mappings;
using BracketMaker.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BracketMaker.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BracketCreatorController(IGenericRepository<Quiz> bracketRepository) : ControllerBase
{
    [HttpGet]
    [Route("get/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await bracketRepository.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Route($"add/")]
    public async Task<IActionResult> AddQuiz(QuizDto quizDto)
    {
        bracketRepository.Add(quizDto.ToQuiz());
        await bracketRepository.SaveAsync();
        return Ok(quizDto);
    }

    [HttpGet]
    [Route("page/")]
    public async Task<IActionResult> GetQuizPage(int page, int pageSize)
    {
        return page < 0 || pageSize <= 0 ? 
            BadRequest() :
            Ok(await bracketRepository.GetPage(pageSize, page));
    }
}