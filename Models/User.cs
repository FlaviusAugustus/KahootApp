using Microsoft.AspNetCore.Identity;

namespace BracketMaker.Models;

public class User : IdentityUser<Guid>, IEntity
{
    public DateTime CreatedAt { get; set; }
}