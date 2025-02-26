using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0304.Models;

namespace Mission08_Team0304.Controllers;

public class HomeController : Controller
{
    private TasksContext _context;

    //constructor
    public HomeController(TasksContext taskTemp) 
    {
        _context = taskTemp;
    }
    
    //viewing the quadrant page
    [HttpGet]
    public IActionResult Quadrants()
    {
        var temp = _context.Tasks.ToList(); //pulls List of tasks
        return View(temp);
    }
    
    //adding a task
    [HttpGet]
    public IActionResult AddATask()
    {
        return View("AddATask", new Task());
    }
    
    //Saves valid new task instances to the database (if invalid, gives errors for correction)
    [HttpPost]
    public IActionResult AddATask(Task response)
    {
        if (ModelState.IsValid)
        {
            _context.Tasks.Add(response); 
            _context.SaveChanges(); 
            
            return View("Confirmation", response);
        }
        else
        {
            return View("AddATask", response);
        }
    }
    
    //pulls up page to edit based on a task chosen by the user
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var recordToEdit = _context.Tasks
            .Single(x => x.TaskId == id);
        
        return View("AddATask", recordToEdit);
    }
    
    //submits edited information back into the database
    [HttpPost]
    public IActionResult Edit(Task updatedInfo)
    {
        _context.Update(updatedInfo);
        _context.SaveChanges();
        
        return RedirectToAction("ViewQuadrants");
    }
    
    //pulls up confirmation page to delete the record
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var recordToDelete = _context.Tasks
            .Single(x => x.TaskId == id);

        return View("Delete", recordToDelete);
    }
    
    //deletes the entry
    [HttpPost]
    public IActionResult Delete(Task deletingInfo)
    {
        _context.Tasks.Remove(deletingInfo);
        _context.SaveChanges();

        return RedirectToAction("ViewQuadrants");
    }
}


}
