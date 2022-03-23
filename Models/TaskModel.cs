namespace AspnetCoreSqliteApi.Models;
public class TaskModel {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Priority { get; set; } // low, medium, high
    public string? Status { get; set; }  // none, started, completed
}