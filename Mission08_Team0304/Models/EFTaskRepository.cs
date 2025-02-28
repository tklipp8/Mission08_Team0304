namespace Mission08_Team0304.Models;

public class EFTaskRepository : ITaskRepository
{
    private TasksContext _context;
    
    public EFTaskRepository(TasksContext temp)
    {
        _context = temp;
    }
    public List<Task> Tasks => _context.Tasks.ToList();
}