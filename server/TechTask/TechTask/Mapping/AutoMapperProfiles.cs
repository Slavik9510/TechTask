using AutoMapper;
using TechTask.DTO;
using TechTask.Models;

namespace TechTask.Mapping;

public class AutoMapperProfiles : Profile
{
	public AutoMapperProfiles()
	{
		CreateMap<AddCustomerDto, Customer>();
		CreateMap<Customer, GetCustomerDto>();
	}
}
