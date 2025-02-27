using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission08_Team0304.Models;
using Task = Mission08_Team0304.Models.Task;

namespace Mission08_Team0304.Controllers;

public class HomeController : Controller
{
    private TasksContext _context;

    //constructor
    public HomeController(TasksContext taskTemp) 
    {
        _context = taskTemp;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var temp = _context.Tasks
            .Include(x => x.CategoryName)
            .Where(x => x.Completed == false) // Only show incomplete tasks
            .ToList();
        
        return View(temp);
        
    }

    //adding a task
    [HttpGet]
    public IActionResult Add()
    {
        ViewBag.Categories = _context.Categories
            .OrderBy(x => x.CategoryName)
            .ToList();
        
        return View("Add", new Task());
    }

    //Saves valid new task instances to the database (if invalid, gives errors for correction)
    [HttpPost]
    public IActionResult Add(Task response)
    {
        if (ModelState.IsValid) 
        {

            _context.Tasks.Add(response); 
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View("Add", response);
        }
    }

    //pulls up page to edit based on a task chosen by the user
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var recordToEdit = _context.Tasks
            .Single(x => x.TaskId == id);
            
        ViewBag.Categories = _context.Categories
            .OrderBy(x => x.CategoryName)
            .ToList();
        
        return View("Add", recordToEdit);
    }

    //submits edited information back into the database
    [HttpPost]
    public IActionResult Edit(Task updatedInfo)
    {
        _context.Update(updatedInfo);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    //pulls up confirmation page to delete the record
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var record = _context.Tasks.Single(x => x.TaskId == id);
        return View(record);
    }

    [HttpPost]
    public IActionResult Delete(Task app)
    {
        _context.Tasks.Remove(app);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Complete(int id)
    {
        var recordToComplete =  _context.Tasks.Find(id);

        if (recordToComplete != null)
        {
            recordToComplete.Completed = true;
            _context.Update(recordToComplete);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

}
