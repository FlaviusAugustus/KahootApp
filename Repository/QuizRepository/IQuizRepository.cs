using BracketMaker.Models;

namespace BracketMaker.Repository.QuizRepository;

public interface IQuizRepository : IGenericRepository<Quiz>
{
    public Task<IEnumerable<QuizDto>> GetQuizzesWithAllTags(IEnumerable<Tag> tags);
    
    public Task<IEnumerable<QuizDto>> GetQuizzesWithOneTag(IEnumerable<Tag> tags);

    public Task<IEnumerable<QuizDto>> GetPageDto(int page, int pageSize);
}