using MongoDB.Driver;

namespace TechTask.Data;

// Represents the MongoDB database context, encapsulating access to the database and its collections
public class MongoDBContext
{
	private readonly IMongoDatabase _database;

	public MongoDBContext(IConfiguration config)
	{
		// retrieve connection string and database name from the configuration
		var connectionString = config.GetValue<string>("MongoDB:ConnectionString");
		var databaseName = config.GetValue<string>("MongoDB:DatabaseName");

		// create MongoDB client and connect to the specified database
		var client = new MongoClient(connectionString);
		_database = client.GetDatabase(databaseName);
	}

	// Generic method to retrieve a MongoDB collection by name
	public IMongoCollection<T> GetCollection<T>(string name)
	{
		return _database.GetCollection<T>(name);
	}
}
