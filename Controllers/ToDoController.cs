using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Models.Frontend;
using TodoApp.Models.ToDoAppDBContext;

namespace ToDoApp.Controllers;

[ApiController]
[Route("")]
public class ToDoController : ControllerBase
{
    private readonly ILogger<ToDoController> Logger;
    private ToDoAppToDbContext AppToDbContext { get; }

    public ToDoController(ILogger<ToDoController> logger, ToDoAppToDbContext appToDbContext)
    {
        Logger = logger;
        AppToDbContext = appToDbContext;
    }

    [HttpGet("todo")]
    public ActionResult<IEnumerable<ToDo>> Get()
    {
        return AppToDbContext.ToDos;
    }

    [HttpPost("todo")]
    public ActionResult<ToDo> Post(ToDoFrontend frontendToDo)
    {
        var todo = new ToDo(frontendToDo);
        if (AppToDbContext.Users.Any(u => u.id == todo.ParentUserId) == false)
        {
            var response = new
            {
                error = true,
                description = $"User with id {todo.ParentUserId} not found"
            };
            return BadRequest(response);
        }
        AppToDbContext.ToDos.Add(todo);
        AppToDbContext.SaveChanges();
        return todo;
        
    }

    [HttpGet("markComplete/{id}")]
    public StatusCodeResult MarkComplete(Guid id)
    {
        try
        {
            var todo = AppToDbContext.ToDos.SingleOrDefault(todo => todo.id == id);
            todo.IsComplete = false;
            AppToDbContext.SaveChanges();
            return StatusCode(202); // same as 58
        }
        catch (Exception e)
        {
            return new StatusCodeResult(404); //same as 54
        }
    }

    [HttpGet("todo/{id}")]
    public ActionResult<ToDo> Get(Guid id)
    {
        // This method throws
        // .SingleOrDefault(todo => todo.id == id);
        var todo = AppToDbContext.ToDos.Find(id);
        
        if (todo == null)
        {
            return NotFound();
        }
        return todo;
    }

    [HttpGet("completed_todo/{id}")]
    public ActionResult<ToDo> GetCompletedToDo(Guid id)
    {
        var todo = AppToDbContext.ToDos.Find(id);
        
        if (todo == null || todo.IsComplete == false)
        {
            return new StatusCodeResult(404);    
        }
        
        return todo;
    }

    [HttpPut("todo")]
    public ActionResult<ToDo> Put(ToDo todoToUpdate)
    {
        var todo = AppToDbContext.ToDos.Find(todoToUpdate.id);
        if (todo == null)
            return new StatusCodeResult(404);
        todo.IsComplete = todoToUpdate.IsComplete;
        todo.Title = todoToUpdate.Title;
        AppToDbContext.SaveChanges(); // !!!!
        return todo;
    }
    
    // from here is the new code
    [HttpGet("users/{userId}/todos")]
    public ActionResult<IEnumerable<ToDo>> GetUsersToDo(Guid userId)
    {
        var user = AppToDbContext.Users.Find(userId);
        if (user == null)
        {
            var response = new
            {
                error = true,
                description = $"User with id {userId} not found"
            };
            return BadRequest(response);
        }
        return AppToDbContext.ToDos.Where(todo => todo.ParentUserId == userId).ToList();
    }
}


/*
 * [{ "hello": "world" }]
 *
 * {"hello": "world"}
 *
 *  - Read about CRUD(either chatGPT or some other resources) (TODO)
 * 
 * - создать эндпоинт прнинимающий id нашей тудушки (гуид) и если такой уже есть, то вернуть нашу туда COMPLETED
 * если нет, то эксепшн "id не найдено" => <<<<<<<сделать новый эндпоинт, с такими же условиями, но если ТуДу не выолнено, вернуть 404 COMPLETED
 * Можешь проверять выполненнность как на уровне БД, так и на уровне языка COMPLETED
 * - миграцию для бд и добавит поле описание и created at (TODO)
 * - создать эндпоинт пут который обновляет все по заданному туду/ если такого нет, то 404 ошибка COMPLETED
 * - сощдать эндпоинт /markComplete/{id}, который изменяет статус туду изкомплит на тру COMPLETED
 * 
*/