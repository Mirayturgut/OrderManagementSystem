namespace OrderManagementSystem.Models;

public class Kitchen
{
    public int Id { get; set; }
    public string KitchenName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; } =  DateTime.Now;
    public Roles Role { get; set; } = Roles.Kitchen;
}