namespace Quiz.Console;
public class DefaultConnectionString : IConnectionString
{
        private string _connectionString;
    public DefaultConnectionString()
    {
            
        _connectionString ="server=localhost;userid=admin;password=admin; database=quiz;";
        
    }
    public string GetConnectionString()
    {
        return _connectionString;
    }

    public void SetConnectionString(string connectionString)
    {
        _connectionString = connectionString;
    }
}