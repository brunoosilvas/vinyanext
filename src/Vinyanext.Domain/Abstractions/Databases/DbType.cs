namespace Vinyanext.Domain.Abstractions.Databases;

public enum DbType
{
    #region Postgresql Database
    PgsqlVyn,
    PgsqlVynRead,
    #endregion

    #region Mongodb Database
    MdbVyn
    #endregion
}
