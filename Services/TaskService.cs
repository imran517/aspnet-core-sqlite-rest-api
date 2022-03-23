using AspnetCoreSqliteApi.Models;

namespace AspnetCoreSqliteApi.Services;
public interface ITaskService
{
    ServiceResponse<IEnumerable<TaskModel>> GetTasks();
    ServiceResponse<TaskModel> GetTask(int id);
    ServiceResponse<int> AddTask(TaskModel task);
    ServiceResponse<int> UpdateTask(TaskModel task);
    ServiceResponse<int> DeleteTask(TaskModel task);
}

public class TaskService : ITaskService
{
    private IDbContext _dbContext;
    public TaskService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ServiceResponse<IEnumerable<TaskModel>> GetTasks()
    {
        var query = "select * from task";
        var queryParams = new Dictionary<string, object>();    
        var tasks = ExecuteQuery(query, queryParams);
        var response = new ServiceResponse<IEnumerable<TaskModel>>{
            Result = true,
            Data = tasks
        };
        return response;
    }

    public ServiceResponse<TaskModel> GetTask(int id)
    {
        var query = "select * from task where id = @id";
        var queryParams = new Dictionary<string, object>
        {
            {"@id", id}
        };   
        var tasks = ExecuteQuery(query, queryParams);
        var response = new ServiceResponse<TaskModel>{
            Result = true,
            Data = tasks.FirstOrDefault()
        };
        return response;
    }

    private IEnumerable<TaskModel> ExecuteQuery(string query, Dictionary<string, object> queryParams)
    {
        var tasks = new List<TaskModel>();
        if (string.IsNullOrEmpty(query.Trim()))
            return tasks;

        using (var conn = _dbContext.GetConnection())
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = query; //"select * from task";
            foreach (var param in queryParams)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(new TaskModel
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = Convert.ToString(reader["name"]),
                    Description = Convert.ToString(reader["description"]),
                    Priority = Convert.ToString(reader["priority"]),
                    Status = Convert.ToString(reader["status"])
                });
            }
            return tasks;
        }
    }

    private int ExecuteCommand(string cmd, Dictionary<string, object> cmdParams)
    {
        int numberOfRowsAffected = 0;
        if (string.IsNullOrEmpty(cmd.Trim()))
            return numberOfRowsAffected;

        using (var conn = _dbContext.GetConnection())
        {
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = cmd;
            foreach (var param in cmdParams)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            numberOfRowsAffected = command.ExecuteNonQuery();
            return numberOfRowsAffected;
        }            
    }

    public ServiceResponse<int> AddTask(TaskModel task)
    {
        var cmd = "insert into task(id, name, description, priority, status) values(@id, @name, @description, @priority, @status)";
        var cmdParams = new Dictionary<string, object>
        {
            {"@id", task.Id},
            {"@name", task.Name??""},
            {"@description", task.Description??""},
            {"@priority", task.Priority??""},
            {"@status", task.Status??""}
        }; 

        var numberOfRowsAffected = ExecuteCommand(cmd, cmdParams);
        var response = new ServiceResponse<int>{
            Result = true,
            Data = numberOfRowsAffected
        };
        return response;
    }

    public ServiceResponse<int> UpdateTask(TaskModel task)
    {
        var cmd = "update task set name = @name, description = @description, priority = @priority, status = @status where id = @id";
        var cmdParams = new Dictionary<string, object>
        {
            {"@id", task.Id},
            {"@name", task.Name??""},
            {"@description", task.Description??""},
            {"@priority", task.Priority??""},
            {"@status", task.Status??""}
        };

        var numberOfRowsAffected = ExecuteCommand(cmd, cmdParams);
        var response = new ServiceResponse<int>{
            Result = true,
            Data = numberOfRowsAffected
        };
        return response;
    }

    public ServiceResponse<int> DeleteTask(TaskModel task)
    {
        var cmd = "delete from task where id = @id";
        var cmdParams = new Dictionary<string, object>
        {
            {"@id", task.Id}
        };

        var numberOfRowsAffected = ExecuteCommand(cmd, cmdParams);
        var response = new ServiceResponse<int>{
            Result = true,
            Data = numberOfRowsAffected
        };
        return response;
    }
}