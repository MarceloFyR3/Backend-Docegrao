using DoceGrao.Api.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoceGrao.Database.Infrastructure.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnType("uniqueidentifier");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)")
                .IsUnicode(false);

            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(c => c.Address)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType("varchar(150)");
            });

            builder.OwnsOne(c => c.Credential, credential =>
            {
                credential.Property(c => c.Login)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasColumnName("Login")
                    .IsUnicode(false);

                credential.Property(c => c.Password)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("Password");


            });

            builder.Property(c => c.SaltKey)
                .IsRequired()
                .HasColumnName("SaltKey");

            builder.OwnsOne(c => c.Document, cpf =>
            {
                cpf.Property(c => c.Number)
                    .IsRequired()
                    .HasColumnName("CPFCNPJ")
                    .HasColumnType("varchar(14)");

                cpf.Property(c => c.DateIssue)
                    .IsRequired()
                    .HasColumnName("DocumentoDateIssue");

                cpf.Property(c => c.Type)
                    .IsRequired()
                    .HasColumnName("DocumentType");
            });


            builder.Property(e => e.Active)
                .HasDefaultValueSql("((0))");

            builder.Property(e => e.Block)
                .HasDefaultValueSql("((0))");

            builder.Property(c => c.DateBlock)
                .HasColumnType("datetime");

            builder.Property(c => c.IncorrectAttempts)
                .HasColumnType("int");

            builder.Property(c => c.DateRegister)
                .HasColumnType("datetime");

            builder.Property(e => e.EmailConfirmed)
                .HasDefaultValueSql("((0))");

            builder.Property(c => c.UrlImg)
                .IsRequired()
                .HasColumnType("varchar(255)");

        }
    }
}
