using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.Models.Frontend;
using TodoApp.Models.ToDoAppDBContext;

namespace ToDoApp.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> Logger;
    private readonly ToDoAppToDbContext AppDbContext;

    public UserController(ToDoAppToDbContext dbContext, ILogger<UserController> logger)
    {
        Logger = logger;
        AppDbContext = dbContext;

    }
    
    [HttpGet("")] 
    public IEnumerable<User> Get()
    {
        return AppDbContext.Users;
    }

    [HttpPost("/user")]
    public async Task<User> Post(UserFrontend userFrontend)
    {
        var user = new User(userFrontend);
        var task = AppDbContext.Users.AddAsync(user);
        // SUPER BIG AND IMPORTNAT ACTION TO DO
        // ...
        Logger.LogInformation("Saving to DB");

        await task;
        await AppDbContext.SaveChangesAsync();
        return user;
    }
    //
    // [HttpGet("users/{id}/todos")]
    // public ActionResult<IEnumerable<ToDo>> GetTodos(User user, ToDo todo)
    // {
    //     return AppDbContext.ToDos.Find(id)
    // }

    [HttpPut("/changeInformation/{id}")]
    public ActionResult<User> Put(Guid id, UserFrontend userFrontend)
    {
        var userId = AppDbContext.Users.Find(id);
        if(userId == null)
        {
            return NotFound();
        }

        userFrontend.Password = userFrontend.Password;
        userFrontend.Age = userFrontend.Age;
        userFrontend.Name = userFrontend.Name;
        AppDbContext.SaveChanges(); 
        return userId;
    }

}