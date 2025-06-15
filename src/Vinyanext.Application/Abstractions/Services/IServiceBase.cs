using Vinyanext.Application.Abstractions.Databases;

namespace Vinyanext.Application.Abstractions.Services;

public interface IServiceBase<T>
{
    IApplicationDbContextBase Context { get; set; }

    T Set(IApplicationDbContextBase context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        return (T)this;
    }
}
