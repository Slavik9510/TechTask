using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTask.Models;

// Customer entity stored in a MongoDB database
public class Customer
{
	// Unique identifier for the customer(ObjectId in MongoDB)
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	[BsonElement("firstName")]
	public string FirstName { get; set; }

	[BsonElement("lastName")]
	public string LastName { get; set; }

	[BsonElement("email")]
	public string Email { get; set; }

	[BsonElement("phoneNumber")]
	public string PhoneNumber { get; set; }

	// Optional address details for the customer
	[BsonElement("address")]
	public Address? Address { get; set; }
}
