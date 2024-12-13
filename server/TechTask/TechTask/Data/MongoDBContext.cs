using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TechTask.Data.Settings;

namespace TechTask.Data;

public class MongoDBContext
{
	private readonly IMongoDatabase _database;

	public MongoDBContext(IOptions<MongoDBSettings> settings)
	{
		var client = new MongoClient(settings.Value.ConnectionString);
		_database = client.GetDatabase(settings.Value.DatabaseName);
	}

	public IMongoCollection<T> GetCollection<T>(string name)
	{
		return _database.GetCollection<T>(name);
	}
}
