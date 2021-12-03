using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfOffre.Tools
{
    internal class PostgreSQL
    {
        private const string host = "localhost";
        private const string username = "postgres";
        private const string password = "root";
        private const string database = "db-architecture-filerouge";

        private readonly NpgsqlConnection con;

        public PostgreSQL()
        {
            var cs = "Host="+ host + ";Username="+ username + ";Password="+ password + ";Database=" + database;
            con = new NpgsqlConnection(cs);
            
        }

        public async Task ExecuteCmd(string sql)
        {
            await con.OpenAsync();
            var cmd = new NpgsqlCommand(sql, con);
            await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }

        public async Task<NpgsqlDataReader> ExecuteSql(string sql)
        {
            var cmd = new NpgsqlCommand(sql,con);
            await con.OpenAsync();
            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            return reader;
        }

        public async Task Close()
        {
            await con.CloseAsync();
        }
    }
}
