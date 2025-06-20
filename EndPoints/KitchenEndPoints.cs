using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
namespace OrderManagementSystem.EndPoints;

public class KitchenEndPoints
{
    private static readonly AppDbContext _context = new AppDbContext();
    public static void PreparedOrderList()
    {
        var orders = _context.Orders
            .Where(x => x.Status == OrderStatus.Pending)
            .ToList();

        foreach (var order in orders)
        {
            Console.WriteLine($"Sipariş #{order.Id} - Durum: {order.Status}" as object);
        }
        Console.ReadKey();
    }

    public static void StatusUpdate()
    {
        Console.Write("Sipariş ID girin: ");
        var id = int.Parse(Console.ReadLine());
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            Console.WriteLine("Sipariş bulunamadı.");
            return;
        }
        Console.Write("Yeni Durum (Hazırlanıyor, Hazır, TeslimEdildi): ");
        var inputNewStatus =  Console.ReadLine();
        if (Enum.TryParse<OrderStatus>(inputNewStatus, true, out var newStatus))
        { //Enum.TryParse kullanarak string değeri enum'a çevirmen gerekiyor.
            order.Status = newStatus;
            _context.SaveChanges();
            Helper.ShowInfoMsg("Sipariş durumu güncellendi.");
        } 
        //Enum.TryParse<OrderStatus>: Girilen string’i OrderStatus enum’ına çevirmeye çalışır.
        //true: büyük/küçük harf duyarsızlık için.
        //out var newStatus: başarılıysa çevrilen enum burada tutulur.
        //order.Status = newStatus: Artık tür uyuşmazlığı kalmaz.
        else
        {
            Console.WriteLine("Geçersiz bir sipariş durumu girdiniz.");
        }
        Console.ReadKey();
    }
}