using HeroesAPI.Collections;
using HeroesAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HeroesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HeroesController : ControllerBase
{
    private readonly IHeroesRepository _repo;

    public HeroesController(IHeroesRepository repository)
    {
        _repo = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var heroes = await _repo.GetAllAsync();
        return Ok(heroes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var hero = await _repo.GetByIdAsync(id);
        if (hero == null)
            return NotFound("Herói não encontrado");
        return Ok(hero);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Hero hero)
    {
        await _repo.CreateAsync(hero);
        return CreatedAtAction(nameof(Get), new { id = hero.Id }, hero);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(Hero hero)
    {
        var hr = await _repo.GetByIdAsync(hero.Id);
        if (hr == null)
            return NotFound("Herói não encontrado");

        await _repo.UpdateAsync(hero);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var hr = await _repo.GetByIdAsync(id);
        if (hr == null)
            return NotFound("Herói não encontrado");
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}