using AutoMapper;
using UserManagementSystem.Api.Models;
using UserManagementSystem.Database.Entities;

namespace UserManagementSystem.Api.Configuration;

public class ProfileMapper: Profile
{
    public ProfileMapper()
    {
        CreateMap<User, UserDto>();
    }
}