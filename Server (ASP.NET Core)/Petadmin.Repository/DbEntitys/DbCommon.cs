using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Petadmin.Repository.DbEntitys
{
    public class DbCommon : IDbCommon
    {
        public string ConnectionString => ConnectionStr;

        public T GenericReaderToMapper<T>(MySqlCommand cmd, Func<MySqlDataReader, T> mapper)
        {
            var obj = default(T);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                obj = mapper(reader);
            }
            return obj;
        }

        public IList<T> GenericReaderToMapperList<T>(MySqlCommand cmd, Func<MySqlDataReader, T> mapper)
        {
            IList<T> list = new List<T>();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(mapper(reader));
            }
            return list;
        }

        public MySqlCommand ProcedureQuery(MySqlConnection connection, string procedure)
        {
            var cmd = new MySqlCommand(procedure, connection) { CommandType = CommandType.StoredProcedure };
            return cmd;
        }
    }
}
