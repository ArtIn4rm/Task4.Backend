using AutoMapper;
using Task4.Application.Common.Mapping;
using Task4.Application.CQRS.RegisteredUsers.Commands.BlockRegisteredUser;

namespace Task4.WebApi.Models
{
    public class BlockingRegisteredUserDto : IMapWith<BlockRegisteredUserCommand>
    {
        public Guid[]? IdList { get; set; }

        public bool IsBlocking { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BlockingRegisteredUserDto, BlockRegisteredUserCommand>()
                .ForMember(userCommand => userCommand.IdList,
                    option => option.MapFrom(userDto => userDto.IdList))
                .ForMember(userCommand => userCommand.IsBlocking,
                    option => option.MapFrom(userDto => userDto.IsBlocking));
        }
    }
}
