using AutoMapper;
using Task4.Application.Common.Mapping;
using Task4.Domain;

namespace Task4.Application.CQRS.RegisteredUsers.Queries.GetAllRegisteredUsers
{
    public class RegisteredUserDto : IMapWith<RegisteredUser>
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? LastAuthorizationDate { get; set; }

        public UserStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisteredUser, RegisteredUserDto>()
                .ForMember(userVm => userVm.Id,
                    option => option.MapFrom(user => user.Id))
                .ForMember(userVm => userVm.Name,
                    option => option.MapFrom(user => user.Name))
                .ForMember(userVm => userVm.Email,
                    option => option.MapFrom(user => user.Email))
                .ForMember(userVm => userVm.RegistrationDate,
                    option => option.MapFrom(user => user.RegistrationDate))
                .ForMember(userVm => userVm.LastAuthorizationDate,
                    option => option.MapFrom(user => user.LastAuthorizationDate))
                .ForMember(userVm => userVm.Status,
                    option => option.MapFrom(user => user.Status));
        }
    }
}
