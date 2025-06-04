using Vinyanext.Application.Abstractions.Database;

namespace Vinyanext.Application.Abstractions.Services;

public interface IServiceBase
{
    IApplicationDbContextBase Context { get; set; }

    T? Set<T>(IApplicationDbContextBase context)
    {
        Context = context;
        return (T?)this;
    }
}
