using MongoDB.Driver;
using TechTask.Interfaces;
using TechTask.Models;

namespace TechTask.Data;

// Implements repository pattern for managing customer data in MongoDB
public class MongoCustomersRepository : ICustomersRepository
{
	private readonly IMongoCollection<Customer> _collection;
	public MongoCustomersRepository(MongoDBContext context)
	{
		// access customers collection in MongoDB
		_collection = context.GetCollection<Customer>("customers");
	}

	// Adds new customer to the database asynchronously
	public async Task AddCustomerAsync(Customer customer)
	{
		await _collection.InsertOneAsync(customer);
	}

	// Retrieves all customers from the database asynchronously
	public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
	{
		// retrieve all documents in the customers collection
		var customers = await _collection.FindAsync(_ => true);
		return await customers.ToListAsync();
	}

	// Retrieves customer by email asynchronously
	// Returns null if no customer with the specified email exists
	public async Task<Customer?> GetCustomerByEmailAsync(string email)
	{
		var customer = await _collection.Find(c => c.Email == email).FirstOrDefaultAsync();
		return customer;
	}

	// Retrieves a customer by phone number asynchronously
	// Returns null if no customer with the specified phone number exists
	public async Task<Customer?> GetCustomerByPhoneNumberAsync(string phoneNumber)
	{
		var customer = await _collection.Find(c => c.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
		return customer;
	}
}
