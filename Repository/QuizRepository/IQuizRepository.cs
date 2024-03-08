using KahootBackend.Models;

namespace KahootBackend.Repository.QuizRepository;

public interface IQuizRepository : IGenericRepository<Quiz>
{
    public Task<IEnumerable<Quiz>> GetQuizzesWithAllTags(IEnumerable<Tag> tags);
    
    public Task<IEnumerable<Quiz>> GetQuizzesWithOneTag(IEnumerable<Tag> tags);

    public Task<Quiz?> GetByIdIncludeAsync(Guid id);
}