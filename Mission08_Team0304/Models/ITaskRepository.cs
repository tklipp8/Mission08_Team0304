using Mission08_Team0304.Models;

public interface ITaskRepository
{
    // Builds the interface to deal with adding, updating, deleting tasks
    // and makes it so tasks are attached to their category
    List<Mission08_Team0304.Models.Task> Tasks { get; }
    IQueryable<Mission08_Team0304.Models.Task> GetTasksWithCategory();
    void AddTask(Mission08_Team0304.Models.Task task);
    Mission08_Team0304.Models.Task GetTaskById(int id);
    void UpdateTask(Mission08_Team0304.Models.Task task);
    void DeleteTask(Mission08_Team0304.Models.Task task);
    List<Category> GetCategories(); // Gets categories
}