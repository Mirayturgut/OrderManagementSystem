namespace OrderManagementSystem.Models;

public class Admin
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AdminName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; } =  DateTime.Now;
    public Roles Role { get; set; } = Roles.Admin;
}