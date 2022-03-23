namespace AspnetCoreSqliteApi.Models;
public class ServiceResponse {
    public bool Result { get; set; }

    public IEnumerable<string> Messages { get; set; } = new List<string>();
}

public class ServiceResponse<T> : ServiceResponse
{
    /// <summary>
    /// The model (Entity or ViewModel)
    /// </summary>
    public T? Data { get; set; }

    public ServiceResponse()
    {
        Data = default;
    }        
}