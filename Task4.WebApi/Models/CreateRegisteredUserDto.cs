using AutoMapper;
using Task4.Application.Common.Mapping;
using Task4.Application.CQRS.RegisteredUsers.Commands.CreateRegisteredUser;

namespace Task4.WebApi.Models
{
    public class CreateRegisteredUserDto : IMapWith<CreateRegisteredUserCommand>
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateRegisteredUserDto, CreateRegisteredUserCommand>()
                .ForMember(userCommand => userCommand.Name,
                    option => option.MapFrom(userDto => userDto.Name))
                .ForMember(userCommand => userCommand.Email,
                    option => option.MapFrom(userDto => userDto.Email))
                .ForMember(userCommand => userCommand.Password,
                    option => option.MapFrom(userDto => userDto.Password));
        }
    }
}
