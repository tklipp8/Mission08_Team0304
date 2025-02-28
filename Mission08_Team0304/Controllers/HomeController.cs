using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0304.Models;
using Task = Mission08_Team0304.Models.Task;

namespace Mission08_Team0304.Controllers;

public class HomeController : Controller
{
    private ITaskRepository _repo;

    // Constructor
    public HomeController(ITaskRepository temp)
    {
        _repo = temp;
    }

    //Pulls up quadrant view of tasks (along with categories)
    [HttpGet]
    public IActionResult Index()
    {
        var tasks = _repo.GetTasksWithCategory().ToList(); // Fetch tasks with categories
        return View(tasks);
    }

    // Loading up the page for adding a task
    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.Categories = _repo.GetCategories(); // Fetch categories from repository
        return View("Add", new Task());
    }

    // Saves valid new task instances to the database, has the user try again with invalid tasks
    [HttpPost]
    public IActionResult Add(Task response)
    {
        if (ModelState.IsValid)
        {
            _repo.AddManager(response); // Use repository to add task
            var tasks = _repo.GetTasksWithCategory().ToList(); // Fetch updated list
            return View("Index", tasks);
        }
        else
        {
            //have the user try again with the current data
            ViewBag.Categories = _repo.GetCategories();
            return View("Add", response);
        }
    }

    // Pulls up page to edit based on a task chosen by the user
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var recordToEdit = _repo.GetTaskById(id); // Fetch task by ID
        ViewBag.Categories = _repo.GetCategories(); // Fetch categories
        return View("Add", recordToEdit);
    }

    // Submits edited information back into the database
    [HttpPost]
    public IActionResult Edit(Task updatedInfo)
    {
        _repo.UpdateTask(updatedInfo); // Use repository to update task
        return RedirectToAction("Index"); //redirects to the quadrants view with the newly added task 
    }

    // Pulls up confirmation page to delete the record
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var record = _repo.GetTaskById(id); // Fetch task by ID
        return View(record);
    }

    //actually deletes the record once confirmed
    [HttpPost]
    public IActionResult Delete(Task app)
    {
        _repo.DeleteTask(app); // Use repository to delete task
        return RedirectToAction("Index");
    }

    
    //allows the user to mark a task as completed
    [HttpPost]
    public IActionResult Complete(int id)
    {
        var recordToComplete = _repo.GetTaskById(id); // Fetch task by ID

        if (recordToComplete != null)
        {
            recordToComplete.Completed = true; // Mark task as completed
            _repo.UpdateTask(recordToComplete); // Use repository to update task
        }

        return RedirectToAction("Index");
    }


}
