using Mapster;

namespace Vinyanext.Application.Abstractions.Mapsters;

public interface IMapster
{
    void Register(TypeAdapterConfig config);
}
