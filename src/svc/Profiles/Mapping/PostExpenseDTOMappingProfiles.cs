using AutoMapper;
using SOLID_Principles.Domain;
using SOLID_Principles.Services.DTOs.Expenses;

namespace SOLID_Principles.Services.Profiles.Mapping;

internal class PostExpenseDTOMappingProfiles : Profile
{
    public PostExpenseDTOMappingProfiles()
    {
        CreateMap<PostExpenseDTO, Expense>();

        CreateMap<Expense, Expense>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
