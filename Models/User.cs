using TodoApp.Models.Frontend;
using TodoApp.Utils;

namespace TodoApp.Models;
public class User
{
    public Guid id { get; set; }
    public String Name { get; set; }
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
    public String Password { get; set; }
    
    private User()
    {
        id = new Guid();
    }

    public User(UserFrontend userFrontend) : this()
    {
        Name = userFrontend.Name;
        Age = userFrontend.Age;
        Password = HashService.ComputeSHA256(userFrontend.Password);
    }
        
}




