using Microsoft.EntityFrameworkCore;
using Mission08_Team0304.Models;

public class EFTaskRepository : ITaskRepository
{
    private TasksContext _context;

    public EFTaskRepository(TasksContext temp)
    {
        _context = temp;
    }

    public List<Mission08_Team0304.Models.Task> Tasks => _context.Tasks.ToList();

    public IQueryable<Mission08_Team0304.Models.Task> GetTasksWithCategory()
    {
        return _context.Tasks.Include(x => x.CategoryName);
    }

    public void AddManager(Mission08_Team0304.Models.Task task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    public Mission08_Team0304.Models.Task GetTaskById(int id)
    {
        return _context.Tasks.Single(x => x.TaskId == id);
    }

    public void UpdateTask(Mission08_Team0304.Models.Task task)
    {
        _context.Update(task);
        _context.SaveChanges();
    }

    public void DeleteTask(Mission08_Team0304.Models.Task task)
    {
        _context.Tasks.Remove(task);
        _context.SaveChanges();
    }

    public List<Category> GetCategories()
    {
        return _context.Categories.OrderBy(x => x.CategoryName).ToList();
    }
}
