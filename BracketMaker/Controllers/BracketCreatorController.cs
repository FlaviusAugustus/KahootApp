using BracketMaker.Models;
using BracketMaker.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BracketMaker.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BracketCreatorController(IGenericRepository<Bracket> bracketRepository) : ControllerBase
{
    [HttpGet]
    [Route("get/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await bracketRepository.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Route($"add/")]
    public async Task<IActionResult> AddBracket(Bracket bracket)
    {
        bracketRepository.Add(bracket);
        await bracketRepository.SaveAsync();
        return Ok(bracket);
    }
}