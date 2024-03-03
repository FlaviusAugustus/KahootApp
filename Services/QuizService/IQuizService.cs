using System.Security.Claims;
using BracketMaker.Models;
using LanguageExt;
using LanguageExt.Common;

namespace BracketMaker.Services.QuizService;

public interface IQuizService
{
    Task<Result<QuizDto>> GetById(Guid quizId);
    Task Add(QuizDto quiz);
    Task AddRange(IEnumerable<QuizDto> quizzes);
    Task<Result<Guid>> Remove(ClaimsPrincipal user, Guid quizId);
    Task<Result<QuizDto>> Update(ClaimsPrincipal user, QuizDto quiz);
    Task<Result<IEnumerable<QuizDto>>> GetPage(int page, int pageSize);
    Task<IEnumerable<QuizDto>> MatchAnyTag(IEnumerable<Tag> tags);
    Task<IEnumerable<QuizDto>> MatchAllTags(IEnumerable<Tag> tags);
    Task<Result<IEnumerable<Quiz>>> GetVirtualize(int startIndex, int count);

    Task<int> GetCount();
}