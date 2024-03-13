using Npgsql;

namespace CloudSql
{
    public class PostgreSqlUnix
    {
        public static NpgsqlConnectionStringBuilder NewPostgreSqlUnixSocketConnectionString(string host, string dbUser, string password, string database)
        {
            // Equivalent connection string:
            // "Server=<INSTANCE_UNIX_SOCKET>;Uid=<DB_USER>;Pwd=<DB_PASS>;Database=<DB_NAME>"
            var connectionString = new NpgsqlConnectionStringBuilder()
            {
                // The Cloud SQL proxy provides encryption between the proxy and instance.
                SslMode = SslMode.Disable,

                // Note: Saving credentials in environment variables is convenient, but not
                // secure - consider a more secure solution such as
                // Cloud Secret Manager (https://cloud.google.com/secret-manager) to help
                // keep secrets safe.
                Host = host, // e.g. '/cloudsql/project:region:instance'
                Username = dbUser, // e.g. 'my-db-user
                Password = password, // e.g. 'my-db-password'
                Database = database, // e.g. 'my-database'
            };
            connectionString.Pooling = true;
            // Specify additional properties here.
            return connectionString;
        }
    }
}