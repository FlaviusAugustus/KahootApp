using BracketMaker.Models;
using BracketMaker.Models.Mappings;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace BracketMaker.Repository.QuizRepository;

public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
{
    public QuizRepository(Context.ItemContext context) : base(context) {}

    public async Task<IEnumerable<QuizDto>> GetQuizzesWithAllTags(IEnumerable<Tag> tags) =>
        await _context.Quizzes
            .Where(q => GetPresentTags(q, tags).Count == q.Tags.Count)
            .Select(q => q.ToDto())
            .ToListAsync();

    public async Task<IEnumerable<QuizDto>> GetQuizzesWithOneTag(IEnumerable<Tag> tags) =>
        await _context.Quizzes
            .Where(q => GetPresentTags(q, tags).Count > 0)
            .Select(q => q.ToDto())
            .ToListAsync();

    private static List<Tag> GetPresentTags(Quiz quiz, IEnumerable<Tag> queryTags) =>
        queryTags
            .Where(t => quiz.Tags.Contains(t))
            .ToList();

    public async Task<IEnumerable<QuizDto>> GetPageDto(int page, int pageSize)
    {
        var res = await GetPage(pageSize, page);
        return res.Select(q => q.ToDto());
    }
}