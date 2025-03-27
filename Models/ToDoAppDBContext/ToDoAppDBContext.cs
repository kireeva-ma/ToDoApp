using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models.ToDoAppDBContext;

public class ToDoAppToDbContext : DbContext
{
    public ToDoAppToDbContext(DbContextOptions<ToDoAppToDbContext> options) : base(options)
    {
    }
    
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<User> Users { get; set; }
}