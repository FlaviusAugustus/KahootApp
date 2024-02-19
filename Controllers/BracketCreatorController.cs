using BracketMaker.AuthHandlers.Requirements;
using BracketMaker.Constants;
using BracketMaker.Models;
using BracketMaker.Models.Mappings;
using BracketMaker.Repository;
using BracketMaker.Repository.QuizRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BracketMaker.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BracketCreatorController(IQuizRepository bracketRepository,
    IAuthorizationService authService) : ControllerBase
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
    [Authorize(Policy = nameof(Policy.CanManageOwnQuizzes))]
    public async Task<IActionResult> AddQuiz(QuizDto quizDto)
    {
        bracketRepository.Add(quizDto.ToQuiz());
        await bracketRepository.SaveAsync();
        return Ok(quizDto);
    }

    [HttpPost]
    [Route("remove/")]
    public async Task<IActionResult> RemoveQuiz(Guid id)
    {
        var quiz = await bracketRepository.GetByIdAsync(id);
        if (quiz is null)
        {
            return NotFound(id);
        }
        var authResult = await authService.AuthorizeAsync(User, quiz, Policy.CanManageOwnQuizzes.ToString());
        if (!authResult.Succeeded)
        {
            return Forbid();
        }
        
        await bracketRepository.RemoveByIdAsync(id);
        await bracketRepository.SaveAsync();
        return Ok(id);
    }

    [HttpGet]
    [Route("page/")]
    public async Task<IActionResult> GetQuizPage(int page, int pageSize)
    {
        return page < 0 || pageSize <= 0 ? 
            BadRequest() :
            Ok(await bracketRepository.GetPage(pageSize, page));
    }

    [HttpGet]
    [Route("get-tag/")]
    public async Task<IActionResult> GetByTags(IEnumerable<Tag> tags)
    {
        var result = await bracketRepository.GetQuizzesWithOneTag(tags);
        return Ok(result);
    }
}