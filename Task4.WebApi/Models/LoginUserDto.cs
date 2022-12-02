using AutoMapper;
using Task4.Application.Common.Mapping;
using Task4.Application.CQRS.RegisteredUsers.Commands.LoginUser;

namespace Task4.WebApi.Models
{
    public class LoginUserDto : IMapWith<LoginUserCommand>
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginUserDto, LoginUserCommand>()
                .ForMember(userCommand => userCommand.Email,
                    option => option.MapFrom(userDto => userDto.Email))
                .ForMember(userCommand => userCommand.Password,
                    option => option.MapFrom(userDto => userDto.Password));
        }
    }
}
