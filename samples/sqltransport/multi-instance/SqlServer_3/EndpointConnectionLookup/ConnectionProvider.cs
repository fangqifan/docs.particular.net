﻿using System.Data.SqlClient;
using System.Threading.Tasks;

#region ConnectionProvider
public static class ConnectionProvider
{
    public static async Task<SqlConnection> GetConnection(string transportAddress)
    {
        var connectionString = GetConnectionString(transportAddress);
        var connection = new SqlConnection(connectionString);
        await connection.OpenAsync()
            .ConfigureAwait(false);
        return connection;
    }

    static string GetConnectionString(string transportAddress)
    {
        var receiverConnectionString = @"Data Source=.\SqlExpress;Database=ReceiverCatalog;Integrated Security=True";
        var senderConnectionString = @"Data Source=.\SqlExpress;Database=SenderCatalog;Integrated Security=True";
        if (transportAddress.StartsWith("Samples.SqlServer.MultiInstanceSender"))
        {
            return senderConnectionString;
        }
        return receiverConnectionString;
    }
}
#endregion
