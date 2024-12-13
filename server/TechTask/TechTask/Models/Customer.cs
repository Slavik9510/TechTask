using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTask.Models;

public class Customer
{
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

	[BsonElement("address")]
	public string? Address { get; set; }
}
