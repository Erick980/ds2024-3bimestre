
using HeroesAPI.Collections;
using MongoDB.Driver;

namespace HeroesAPI.Repositories;

public class HeroesRepository : IHeroesRepository
{
    private readonly IMongoCollection<Hero> _collection;
    public HeroesRepository(IMongoDatabase mongoDatabase)
    {
        _collection = mongoDatabase.GetCollection<Hero>("produtos");
    }

    public async Task CreateAsync(Hero hero) =>
        await _collection.InsertOneAsync(hero);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(h => h.Id == id);

    public async Task<List<Hero>> GetAllAsync() =>
        await _collection.Find(h => true).ToListAsync();

    public async Task<Hero> GetByIdAsync(string id) =>
        await _collection.Find(h => h.Id == id).FirstOrDefaultAsync();

    public async Task UpdateAsync(Hero hero) =>
        await _collection.ReplaceOneAsync(h => h.Id == hero.Id, hero);
}