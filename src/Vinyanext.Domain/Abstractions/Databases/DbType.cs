namespace Vinyanext.Domain.Abstractions.Databases;

public enum DbType
{
    #region Postgresql Database
    PgsqlVinyanext,
    PgsqlVinyanextRead,
    #endregion

    #region Mongodb Database
    MdbVinyanext
    #endregion
}
