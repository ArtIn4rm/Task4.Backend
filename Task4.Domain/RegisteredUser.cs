using System.ComponentModel.DataAnnotations;

namespace Task4.Domain
{
    public class RegisteredUser
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? LastAuthorizationDate { get; set;}

        public UserStatus Status { get; set; }
    }

    public enum UserStatus
    {
        Active,
        Blocked
    }
}
