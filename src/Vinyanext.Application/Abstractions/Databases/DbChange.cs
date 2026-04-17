using Vinyanext.Domain.Abstractions.Databases;

namespace Vinyanext.Application.Abstractions.Databases;

public delegate IDbContext ChangeDbContext(DbType type);
public delegate IMdbContext ChangeMdbContext(DbType type);
