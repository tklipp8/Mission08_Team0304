using Microsoft.EntityFrameworkCore;
using Mission08_Team0304.Models;

public class EFTaskRepository : ITaskRepository
{
    //Variable for the context so all the classes in here can access it
    private TasksContext _context;

    //Makes class for the repository itself
    public EFTaskRepository(TasksContext temp)
    {
        _context = temp;
    }

    // Gets all the tasks in a list
    public List<Mission08_Team0304.Models.Task> Tasks => _context.Tasks.ToList();

    // Allows the Tasks and Category tables to connect properly
    public IQueryable<Mission08_Team0304.Models.Task> GetTasksWithCategory()
    {
        return _context.Tasks.Include(x => x.CategoryName);
    }

    // Adds a task and saves it to the database
    public void AddTask(Mission08_Team0304.Models.Task task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    // Finds a particular task using its id
    public Mission08_Team0304.Models.Task GetTaskById(int id)
    {
        return _context.Tasks.Single(x => x.TaskId == id);
    }

    // Makes edits to a task and saves it
    public void UpdateTask(Mission08_Team0304.Models.Task task)
    {
        _context.Update(task);
        _context.SaveChanges();
    }

    // Removes a task from the database and saves it
    public void DeleteTask(Mission08_Team0304.Models.Task task)
    {
        _context.Tasks.Remove(task);
        _context.SaveChanges();
    }

    // Gets a list of categories and puts them in order
    public List<Category> GetCategories()
    {
        return _context.Categories.OrderBy(x => x.CategoryName).ToList();
    }
}