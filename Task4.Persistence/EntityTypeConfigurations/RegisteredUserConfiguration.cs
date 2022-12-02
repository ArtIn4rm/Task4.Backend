using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task4.Domain;

namespace Task4.Persistence.EntityTypeConfigurations
{
    public class RegisteredUserConfiguration : IEntityTypeConfiguration<RegisteredUser>
    {
        public void Configure(EntityTypeBuilder<RegisteredUser> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();
            builder.Property(user => user.Name).HasMaxLength(40);
            builder.Property(user => user.Email).HasMaxLength(30);
            builder.Property(user => user.PasswordHash).HasMaxLength(65);
        }
    }
}
