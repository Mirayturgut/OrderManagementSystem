using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;
namespace OrderManagementSystem.EndPoints;

public class AdminEndPoint
{
    private static readonly AppDbContext _context = new AppDbContext();
    public static void AddProduct ()
    {
        Console.WriteLine("-----Ürün Ekle-----");
        var inputÜrünAdı = Helper.Ask("Ürün Adı");
        var inputFiyat = Decimal.Parse(Helper.Ask("Fiyat"));
        var inputStok = decimal.Parse(Helper.Ask("Stok"));

        var product = new Product
        {
            Name = inputÜrünAdı,
            Price = inputFiyat,
            Stock = inputStok
        };
        _context.Products.Add(product);
        _context.SaveChanges();
        Helper.ShowInfoMsg("Ürün başarıyla eklendi.");
    }
    public static void ProductUpdate()
    {
        using var  context = new AppDbContext();
        Console.Clear();
        Console.WriteLine("-----Ürün Güncelle-----");
        var products  = _context.Products.ToList();
        if (products.Count == 0)
        {
            Console.WriteLine("Düzenlenecek ürün kaydı bulunmamaktadır.");
            Thread.Sleep(2000);
            return;
        }

        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name}");
        }
        var secim = Helper.AskNumber("Düzenlemek istediğiniz kaydın numarasını girin:");
        if (secim < 0 || secim >= products.Count)
        {
            Helper.ShowErrorMsg("Geçersiz seçim.Ana menüye dönülüyor.");
            Thread.Sleep(2000);
            return; 
        }
        var secilenÜrün =  products[secim -1];
        Console.WriteLine($"Seçilen Ürün: {secilenÜrün.Name} / {secilenÜrün.Price} / {secilenÜrün.Stock}");
        Console.WriteLine("----------------");

        var yeniÜrünAdı = Helper.Ask("Yeni Ürün Adı");
        var yeniÜrünFiyatı = Helper.Ask("Yeni Ürün Fiyatı");
        var yeniÜrünStok = Helper.Ask("Yeni Ürün Stok");
        _context.SaveChanges();
        Helper.ShowInfoMsg("Ürün bilgisi başarıyla güncellendi.");
    }
    public static void ProductList()
    {
        using var  context = new AppDbContext();
        var products = _context.Products.ToList();
        Console.Clear();
        Console.WriteLine("-----Ürün Listesi-----");
        if (products.Count == 0)
        {
            Console.WriteLine("Kayıtlı ürün yok.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Ürünün Adı:{product.Name} / Ürünün Fiyatı:{product.Price} / Ürünün Stok Durumu: {product.Stock}");
            }
        }
        Helper.ShowInfoMsg("Devam etmek için bir tuşa basınız.");
        Console.ReadKey();
    }
    public static void AllList()
    {
        var users = _context.Users.ToList();
        Console.WriteLine("Tüm Kayıtlı Kullanıcılar");
        foreach (var user in users)
        {
            Console.WriteLine($"Ad: {user.Name} - Kullanıcı Adı: {user.Username} - Rol:{user.Role}");
        }
        var orders = _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .ThenInclude(x => x.Product)
            .ToList();
        Console.WriteLine("Tüm Siparişler");
        foreach (var order in orders)
        {
            Console.WriteLine($"\nSipariş ID: {order.Id} | Kullanıcı: {order.User?.Name} | Tarih: {order.OrderDate}");

            foreach (var item in order.OrderItems)
            {
                Console.WriteLine($" - Ürün: {item.Product?.Name} | Adet: {item.Quantity} | Fiyat: {item.Product?.Price:C}");
            }
        }

        Console.ReadKey();
    }
}