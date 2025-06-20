namespace OrderManagementSystem.Models;

public class User
{
    public  int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }  = string.Empty;
    public DateTime CreatedDate { get; set; } =  DateTime.Now;
    public Roles Role { get; set; } = Roles.User;
    public List<Order> Orders { get; set; }
}