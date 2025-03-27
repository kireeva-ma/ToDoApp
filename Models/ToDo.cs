using Microsoft.EntityFrameworkCore;
using TodoApp.Models.Frontend;

namespace TodoApp.Models;

public class ToDo
{
    public Guid id { get; set; }
    public String Title { get; set; }
    public Boolean IsComplete { get; set; }
    
    public Guid ParentUserId { get; set; }

    private ToDo()
    {
        id = new Guid();
    }

    public ToDo(ToDoFrontend toDoFrontend) : this()
    {
        Title = toDoFrontend.Title;
        IsComplete = toDoFrontend.IsComplete;
        ParentUserId = toDoFrontend.ParentUserId;
    }
}



