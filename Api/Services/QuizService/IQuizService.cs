using System.Security.Claims;
using KahootBackend.Models;
using LanguageExt;
using LanguageExt.Common;

namespace KahootBackend.Services.QuizService;

public interface IQuizService
{
    Task<Result<Quiz>> GetById(Guid quizId);
    Task<Result<Quiz>> GetByIdInclude(Guid quizId);
    Task Add(QuizDto quiz);
    Task AddRange(IEnumerable<QuizDto> quizzes);
    Task<Result<Guid>> Remove(ClaimsPrincipal user, Guid quizId);
    Task<Result<Quiz>> Update(ClaimsPrincipal user, QuizDto quiz);
    Task<Result<IEnumerable<Quiz>>> GetPage(int page, int pageSize);
    Task<IEnumerable<Quiz>> MatchAnyTag(IEnumerable<Tag> tags);
    Task<IEnumerable<Quiz>> MatchAllTags(IEnumerable<Tag> tags);
    Task<Result<IEnumerable<Quiz>>> GetVirtualize(int startIndex, int count);

    Task<int> GetCount();
}