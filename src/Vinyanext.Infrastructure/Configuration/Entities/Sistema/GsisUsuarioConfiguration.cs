using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vinyanext.Domain.Entities.Sistema;

namespace Vinyanext.Infrastructure.Configuration.Entities.Sistema;

internal sealed class GsisUsuarioConfiguration : IEntityTypeConfiguration<GsisUsuario>
{
    public void Configure(EntityTypeBuilder<GsisUsuario> builder)
    {
        throw new NotImplementedException();
    }
}
