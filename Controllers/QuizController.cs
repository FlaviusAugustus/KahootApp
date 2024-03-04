using BracketMaker.Constants;
using BracketMaker.Models;
using BracketMaker.Services.QuizService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BracketMaker.Controllers;

[ApiController]
[Route("/api/quiz")]
public class QuizController(IQuizService quizService) : ControllerBase
{
    [HttpGet]
    [Route("get/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await quizService.GetById(id);
        return result.Match<IActionResult>(
            success => Ok(success),
            fail => NotFound(fail.Message));
    }

    [HttpPost]
    [Route($"add/")]
    [Authorize(Policy = nameof(Policy.CanManageQuizzesGlobally))]
    public async Task<IActionResult> AddQuiz(QuizDto quizDto)
    {
        await quizService.Add(quizDto);
        return Ok(quizDto);
    }

    [HttpPost]
    [Route("add-range")]
    [Authorize(Policy = nameof(Policy.CanManageQuizzesGlobally))]
    public async Task<IActionResult> AddRange(IEnumerable<QuizDto> quizzes)
    {
        await quizService.AddRange(quizzes);
        return Ok(quizzes);
    }

    [HttpDelete]
    [Route("remove/")]
    public async Task<IActionResult> RemoveQuiz(Guid id)
    {
        var result = await quizService.Remove(User, id);
        return result.Match<IActionResult>(
            success => success == Guid.Empty ? NotFound() : Ok(),
            fail => Forbid(fail.Message));
    }
    
    [HttpPut]
    [Route("update/")]
    public async Task<IActionResult> UpdateQuiz(QuizDto quiz)
    {
        var result = await quizService.Update(User, quiz);
        return result.Match<IActionResult>(
            success => success is null ? NotFound() : Ok(),
            fail => Forbid(fail.Message));
    }

    [HttpGet]
    [Route("page/")]
    public async Task<IActionResult> GetQuizPage(int page, int pageSize)
    {
        var result = await quizService.GetPage(page, pageSize);
        return result.Match<IActionResult>(
            success => Ok(success),
            fail => NotFound(fail.Message));
    }

    [HttpGet]
    [Route("get-virtualize")]
    public async Task<IActionResult> GetVirtualize(int startIndex, int count)
    {
        var result = await quizService.GetVirtualize(startIndex, count);
        return result.Match<IActionResult>(
            success => Ok(success),
            fail => NotFound(fail.Message));
    }

    [HttpGet]
    [Route("count")]
    public async Task<IActionResult> GetCount()
    {
        var count = await quizService.GetCount();
        return Ok(count);
    }

    [HttpGet]
    [Route("get-any-tag/")]
    public async Task<IActionResult> MatchAnyTag(IEnumerable<Tag> tags)
    {
        var matches = await quizService.MatchAnyTag(tags);
        return Equals(matches, Enumerable.Empty<Quiz>()) ? 
                Ok(matches) :
                NotFound();
    }
    
    [HttpGet]
    [Route("get-all-tags/")]
    public async Task<IActionResult> MatchAllTags(IEnumerable<Tag> tags)
    {
        var matches = await quizService.MatchAllTags(tags);
        return Equals(matches, Enumerable.Empty<Quiz>()) ? 
                Ok(matches) :
                NotFound();
    }
}