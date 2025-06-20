using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
namespace OrderManagementSystem.EndPoints;

public class UserEndPoint
{
    private static readonly AppDbContext _context = new AppDbContext();
    public static void ListOfProducts()
    {
        var products = _context.Products.ToList();
        foreach (var product in products)
        {
            Console.WriteLine($" Ürün: {product.Name} - Fiyatı:{product.Price:C}");
        }
        Console.ReadKey();
    }

    public static void CreateOrder(User loggedInUser)
    {
        var products = _context.Products.ToList();
        Console.WriteLine("Ürünler:");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id} - {product.Name} ({product.Price}₺)");
        }

        var order = new Order
        {
            UserId = loggedInUser.Id,
            Status = OrderStatus.Pending,
            OrderDate = DateTime.Now,
            OrderItems = new List<OrderItem>()
        };

        while (true)
        {
            Console.WriteLine("Ürün ID giriniz:");
            var id = Console.ReadLine();
            if (!int.TryParse(id, out int productId))
            {
                Helper.ShowErrorMsg("Geçerli bir ürün ID giriniz.");
                return;
            }

            var selectedProduct = _context.Products.Find(productId);
            if (selectedProduct == null)
            {
                Helper.ShowErrorMsg("Ürün bulunamadı.");
                continue;
            }

            Console.WriteLine("Adet: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Helper.ShowErrorMsg("Geçersiz adet.");
                continue;
            }

            order.OrderItems.Add(new OrderItem
            {
                ProductId = selectedProduct.Id,
                Quantity = quantity,
                UnitPrice = selectedProduct.Price // bu alan varsa ekleyin
            });

            break;
        }

        if (order.OrderItems.Count > 0)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            Helper.ShowSuccessMsg("Sipariş oluşturuldu.");
        }
        else
        {
            Helper.ShowInfoMsg("Hiç ürün seçilmedi. Sipariş iptal edildi.");
        }

        Console.ReadKey();
    }
    
}