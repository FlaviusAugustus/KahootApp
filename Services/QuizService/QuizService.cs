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
    public async Task<Result<Quiz>> GetById(Guid id)
    {
        var quiz = await quizRepository.GetByIdAsync(id);
        return quiz is null ? 
            new Result<Quiz>(new ArgumentException("No such quiz exists")) :
            new Result<Quiz>(quiz);
    }
    
    public async Task<Result<Quiz>> GetByIdInclude(Guid id)
    {
        var quiz = await quizRepository.GetByIdIncludeAsync(id);
        return quiz is null ? 
            new Result<Quiz>(new ArgumentException("No such quiz exists")) :
            new Result<Quiz>(quiz);
    }

    public async Task Add(QuizDto quiz)
    {
        quizRepository.Add(quiz.ToQuiz());
        await quizRepository.SaveAsync();
    }

    public async Task AddRange(IEnumerable<QuizDto> quizzes)
    {
        await quizRepository.AddRange(quizzes.Select(q => q.ToQuiz()));
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

    public async Task<Result<Quiz>> Update(ClaimsPrincipal user, QuizDto quizDto) => 
        await ManageQuiz<Quiz>(user, quizDto.ToQuiz(), quizRepository.Update);

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

    public async Task<Result<IEnumerable<Quiz>>> GetPage(int page, int pageSize)
    {
        if (page < 0 || pageSize <= 0)
        {
            var exception = new ArgumentException("Incorrect page or page size");
            return new Result<IEnumerable<Quiz>>(exception);
        }

        var quizPage = await quizRepository.GetPage(pageSize, page);
        return new Result<IEnumerable<Quiz>>(quizPage);
    }

    public async Task<IEnumerable<Quiz>> MatchAnyTag(IEnumerable<Tag> tags) =>
        await quizRepository.GetQuizzesWithOneTag(tags);

    public async Task<IEnumerable<Quiz>> MatchAllTags(IEnumerable<Tag> tags) =>
        await quizRepository.GetQuizzesWithAllTags(tags);

    public async Task<Result<IEnumerable<Quiz>>> GetVirtualize(int startIndex, int count)
    {
        if (startIndex < 0 || count < 0)
        {
            var exception = new ArgumentException("Incorrect arguments for virtualize");
            return new Result<IEnumerable<Quiz>>(exception);
        }

        var items = await quizRepository.GetVirtualize(startIndex, count);
        return new Result<IEnumerable<Quiz>>(items);
    }

    public Task<int> GetCount() =>
        quizRepository.GetItemCount();
}