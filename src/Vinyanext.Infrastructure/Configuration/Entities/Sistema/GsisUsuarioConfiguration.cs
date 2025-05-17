using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vinyanext.Domain.Entities.Sistema;

namespace Vinyanext.Infrastructure.Configuration.Entities.Sistema;

internal sealed class GsisUsuarioConfiguration : IEntityTypeConfiguration<GsisUsuario>
{
    public void Configure(EntityTypeBuilder<GsisUsuario> builder)
    {
        builder.ToTable("gsis_usuario");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Cpf).HasColumnName("cpf");
        builder.Property(t => t.Senha).HasColumnName("senha");
    }
}
