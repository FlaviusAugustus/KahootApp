using BracketMaker.Models;
using Microsoft.EntityFrameworkCore;

namespace BracketMaker.Repository.QuizRepository;

public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
{
    public QuizRepository(Context.ItemContext context) : base(context) {}

    public async Task<IEnumerable<Quiz>> GetQuizzesWithAllTags(IEnumerable<Tag> tags) =>
        await _context.Quizzes
            .Where(q => GetPresentTags(q, tags).Count == q.Tags.Count)
            .ToListAsync();

    public async Task<IEnumerable<Quiz>> GetQuizzesWithOneTag(IEnumerable<Tag> tags) =>
        await _context.Quizzes
            .Where(q => GetPresentTags(q, tags).Count > 0)
            .ToListAsync();

    private static List<Tag> GetPresentTags(Quiz quiz, IEnumerable<Tag> queryTags) =>
        queryTags
            .Where(t => quiz.Tags.Contains(t))
            .ToList();
}