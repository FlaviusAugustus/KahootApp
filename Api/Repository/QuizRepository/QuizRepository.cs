using KahootBackend.Models.Mappings;
using KahootBackend.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace KahootBackend.Repository.QuizRepository;

public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
{
    public QuizRepository(Context.ItemContext context) : base(context) {}

    public async Task<IEnumerable<Quiz>> GetQuizzesWithAllTags(IEnumerable<Tag> tags) =>
        await _context.Quizzes
            .ToListAsync();

    public async Task<IEnumerable<Quiz>> GetQuizzesWithOneTag(IEnumerable<Tag> tags) =>
        await _context.Quizzes
            .ToListAsync();

    public async Task<Quiz?> GetByIdIncludeAsync(Guid id) =>
        await _context.Quizzes
            .Include(q => q.Tags)
            .Include(q => q.Questions)
            .ThenInclude(q => q.Choices)
            .SingleOrDefaultAsync(q => q.Id == id);
            

    private static List<Tag> GetPresentTags(Quiz quiz, IEnumerable<Tag> queryTags) =>
        queryTags
            .Where(t => quiz.Tags.Contains(t))
            .ToList();

    public async Task<IEnumerable<QuizDto>> GetPageDto(int page, int pageSize)
    {
        return Enumerable.Empty<QuizDto>();
    }
}