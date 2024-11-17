using System;
using AutoMapper;
using SOLID_Principles.Domain;
using SOLID_Principles.Services.DTOs.Users;

namespace SOLID_Principles.Services.Profiles.Mapping;

public class PostUserDTOMappingProfiles : Profile
{
    public PostUserDTOMappingProfiles()
    {
        CreateMap<PostUserDTO, User>();
    }
}
