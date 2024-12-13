using MongoDB.Driver;
using TechTask.Interfaces;
using TechTask.Models;

namespace TechTask.Data;

public class MongoCustomersRepository : ICustomersRepository
{
	private readonly IMongoCollection<Customer> _collection;
	public MongoCustomersRepository(MongoDBContext context)
	{
		_collection = context.GetCollection<Customer>("customers");
	}

	public async Task AddCustomerAsync(Customer customer)
	{
		await _collection.InsertOneAsync(customer);
	}

	public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
	{
		var customers = await _collection.FindAsync(_ => true);
		return await customers.ToListAsync();
	}

	public async Task<Customer?> GetCustomerByEmailAsync(string email)
	{
		var customer = await _collection.Find(c => c.Email == email).FirstOrDefaultAsync();
		return customer;
	}

	public async Task<Customer?> GetCustomerByPhoneNumberAsync(string phoneNumber)
	{
		var customer = await _collection.Find(c => c.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
		return customer;
	}
}
