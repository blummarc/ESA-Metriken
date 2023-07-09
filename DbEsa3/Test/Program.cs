using System.Data;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.Collections.Generic;

static void Main(string[] args)
{
    // Oracle-Verbindungszeichenfolge
    string connectionString = "Data Source=localhost:1521/rispdb1;User ID=s87799;Password=student;";

    OracleConnection connection = new OracleConnection(connectionString);

    connection.Open();

    string sql = "select ANONID,Query,Querytime from aoldata.querydata where query like '%immigration law%";

    OracleCommand command = new OracleCommand(sql, connection);

    OracleDataAdapter adapter = new OracleDataAdapter(command);

    DataTable table = new DataTable();

    adapter.Fill(table);

    string path = @"E:\Testpath\to\output.csv";

    using (StreamWriter writer = new StreamWriter(path))
    {
        foreach (DataRow row in table.Rows)
        {
            string line = string.Join(",", row.ItemArray);

            writer.WriteLine(line);
        }
    }
}

