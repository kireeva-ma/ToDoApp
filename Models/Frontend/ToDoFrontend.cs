namespace TodoApp.Models.Frontend;

public class ToDoFrontend
{
    public string Title { get; set; }
    public bool IsComplete { get; set; }
    
    public Guid ParentUserId { get; set; }
}
