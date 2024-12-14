using AutoMapper;
using TechTask.DTO;
using TechTask.Models;

namespace TechTask.Mapping;

// Automapper profile for configuring object-to-object mappings
public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{
		CreateMap<Address, AddressDto>().ReverseMap(); // bidirectional mapping
		CreateMap<AddCustomerDto, Customer>();
		CreateMap<Customer, GetCustomerDto>();
	}
}
