using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace BracketMaker.Models;

public class User : IdentityUser<Guid>, IEntity
{
    public DateTime CreatedAt { get; set; }

    public IList<Quiz> OwnedQuizzes { get; set; } = new List<Quiz>();
}