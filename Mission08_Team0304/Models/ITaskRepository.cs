using Mission08_Team0304.Models;

public interface ITaskRepository
{
    List<Mission08_Team0304.Models.Task> Tasks { get; }
    IQueryable<Mission08_Team0304.Models.Task> GetTasksWithCategory();
    void AddManager(Mission08_Team0304.Models.Task task);
    Mission08_Team0304.Models.Task GetTaskById(int id);
    void UpdateTask(Mission08_Team0304.Models.Task task);
    void DeleteTask(Mission08_Team0304.Models.Task task);
    List<Category> GetCategories(); // Fetch categories
}