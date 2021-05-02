using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Petadmin.Repository.DbEntitys
{
    public interface IDbCommon
    {
        string ConnectionString { get; }

        /// <summary>
        /// Create MySql cmd from procedure string.
        /// </summary>
        /// <param name="connection"> MySql Connection</param>
        /// <param name="procedure"> procedure string</param>
        /// <returns>
        /// Return MySqlCommand Type.
        /// </returns>
        MySqlCommand ProcedureQuery(MySqlConnection connection, string procedure);

        /// <summary>
        /// Genaric reading from MySqlDataReader to Object Type T
        /// </summary>
        /// <param name="cmd"> MySql Command</param>
        /// <param name="mapper"> Func mapper maps from MySqlDataReader to Object Type T</param>
        /// <returns>
        /// Return new Object Type T
        /// </returns>
        T GenericReaderToMapper<T>(MySqlCommand cmd, Func<MySqlDataReader, T> mapper);

        /// <summary>
        /// Genaric reading from MySqlDataReader to List of Objects Type T
        /// </summary>
        /// <param name="cmd"> MySql Command</param>
        /// <param name="mapper"> Func mapper maps from MySqlDataReader to Object Type T</param>
        /// <returns>
        /// Return new List of Objects Type T
        /// </returns>
        IList<T> GenericReaderToMapperList<T>(MySqlCommand cmd, Func<MySqlDataReader, T> mapper);
    }
}
