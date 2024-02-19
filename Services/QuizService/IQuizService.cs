using BracketMaker.Models;
using LanguageExt;
using LanguageExt.Common;

namespace BracketMaker.Services.QuizService;

public interface IQuizService
{
    Task<Result<bool>> GetById(Guid quizId);

    Task<Result<Unit>> Add(QuizDto quiz);
}