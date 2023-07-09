using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbEsa3
{
    internal class Query
    {
        string[] queries;
        public Query(params string[] queries)
        {
            this.queries = queries;
        }

        public DataTable CreateTable(OracleConnection conn)
        {
            DataTable table = new DataTable();
            using (OracleCommand cmd = conn.CreateCommand())
            {
                for (int i = 0; i < queries.Length; i++)
                {
                    if (queries[i] != queries.Last())
                    {
                        cmd.CommandText = queries[i];
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = queries[i];
                        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                        adapter.Fill(table);
                    }
                }
            }
            return table;
        }
    }
}

