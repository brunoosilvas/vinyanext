using Vinyanext.Application.Abstractions.Databases;

namespace Vinyanext.Application.Abstractions.Services;

public interface IServiceBase<T>
{
    IDbBaseContext BaseContext { get; set; }

    T Set(IDbBaseContext baseContext)
    {
        BaseContext = baseContext ?? throw new ArgumentNullException(nameof(baseContext));
        return (T)this;
    }
}
