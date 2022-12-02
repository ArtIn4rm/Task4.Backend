using AutoMapper;
using Task4.Application.Common.Mapping;
using Task4.Application.CQRS.RegisteredUsers.Commands.DeleteRegisteredUser;

namespace Task4.WebApi.Models
{
    public class DeleteRegisteredUserDto : IMapWith<DeleteRegisteredUserCommand>
    {
        public Guid[]? IdList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteRegisteredUserDto, DeleteRegisteredUserCommand>()
                .ForMember(userCommand => userCommand.IdList,
                    option => option.MapFrom(userDto => userDto.IdList));
        }
    }
}
