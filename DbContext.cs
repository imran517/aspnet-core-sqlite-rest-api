using Microsoft.Data.Sqlite;

namespace AspnetCoreSqliteApi;
public interface IDbContext
{
    SqliteConnection GetConnection();
}

public class DbContext : IDbContext
{
    private string dbFileName;

    private string connectionString;
    public DbContext()
    {
        dbFileName = "taskdb.sqlite";
        connectionString = $"Data Source={dbFileName}";
    }

    public SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(connectionString);
        return connection;
    }
}