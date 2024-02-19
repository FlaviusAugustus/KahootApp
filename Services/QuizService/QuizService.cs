using System.Security.Claims;
using BracketMaker.Constants;
using BracketMaker.Models;
using BracketMaker.Models.Mappings;
using BracketMaker.Repository.QuizRepository;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;

namespace BracketMaker.Services.QuizService;

public class QuizService(IQuizRepository quizRepository,
    IAuthorizationService authService) : IQuizService
{
    public async Task<Result<QuizDto>> GetById(Guid id)
    {
        var quiz = await quizRepository.GetByIdAsync(id);
        return quiz is null ? 
            new Result<QuizDto>(new ArgumentException("No such quiz exists")) :
            new Result<QuizDto>(quiz.ToDto());
    }

    public async Task Add(QuizDto quiz)
    {
        quizRepository.Add(quiz.ToQuiz());
        await quizRepository.SaveAsync();
    }

    public async Task<Result<Guid>> Remove(ClaimsPrincipal user, Guid id)
    {
        var quiz = await quizRepository.GetByIdAsync(id);
        if (quiz is null)
        {
            return new Result<Guid>(Guid.Empty);
        }
        return await ManageQuiz<Guid>(user, quiz, quizRepository.Remove);
    }

    public async Task<Result<QuizDto>> Update(ClaimsPrincipal user, QuizDto quizDto) => 
        await ManageQuiz<QuizDto>(user, quizDto.ToQuiz(), quizRepository.Update);

    private async Task<Result<T>> ManageQuiz<T>(ClaimsPrincipal user, Quiz quiz,
        Action<Quiz> quizManager)
    {
        var authResult = await authService.AuthorizeAsync(user, quiz, Policy.CanManageOwnQuizzes.ToString());
        if (!authResult.Succeeded)
        {
            var exception = new ArgumentException("Access denied");
            return new Result<T>(exception);
        }

        quizManager(quiz);
        await quizRepository.SaveAsync();
        return new Result<T>();
    }

    public async Task<Result<IEnumerable<QuizDto>>> GetPage(int page, int pageSize)
    {
        if (page < 0 || pageSize <= 0)
        {
            var exception = new ArgumentException("Incorrect page or page size");
            return new Result<IEnumerable<QuizDto>>(exception);
        }

        var quizPage = await quizRepository.GetPageDto(pageSize, page);
        return new Result<IEnumerable<QuizDto>>(quizPage);
    }

    public async Task<IEnumerable<QuizDto>> MatchAnyTag(IEnumerable<Tag> tags) =>
        await quizRepository.GetQuizzesWithOneTag(tags);

    public async Task<IEnumerable<QuizDto>> MatchAllTags(IEnumerable<Tag> tags) =>
        await quizRepository.GetQuizzesWithAllTags(tags);
}